using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class TeamListViewModel : ViewModelBase
{
    [ObservableProperty] private string _personName;
    [ObservableProperty] private string _number;
    [ObservableProperty] private ObservableCollection<Team> _teamList;
    [ObservableProperty] [Required(ErrorMessage = "يجب إضافة اسم")] [NotifyDataErrorInfo] 
    private string _nameEdit;
    [ObservableProperty] [Required(ErrorMessage = "مطلوب رقم الواتساب لكي يتواصل معك مالك التيم")] [PhoneNumber(ErrorMessage = "رقم الهاتف غير صحيح")] [NotifyDataErrorInfo]
    private string _numberEdit;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsVisible))]
    private bool _isEditVisible;
    [ObservableProperty]
    private bool _isMobile;
    private int PersonId { get; set; }
    private HttpClient client;
    private HubConnection connection;
    private string BaseURL = "https://teamsearcher-production.up.railway.app";
    public bool IsVisible
    {
        get => IsEditVisible;
        set => IsEditVisible = value;
    }

    public TeamListViewModel(int id)
    {
        client = new HttpClient();
        TeamList = new ObservableCollection<Team>();
        IsEditVisible = false;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
        {
            IsMobile = MobileInputHelper.IsMobile();

            if (IsMobile)
            {
                MobileInputHelper.TextReceived += OnMobileTextReceived;
                MobileInputHelper.BackspacePressed += OnMobileBackspacePressed;
            }
        }
        
        connection = new HubConnectionBuilder()
            .WithUrl($"{BaseURL}/personHub?userId={id}")
            .WithAutomaticReconnect()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions = AppJsonContext.Default.Options;
            })
            .Build();

        connection.On("GetTeams", async () => await GetTeams(id));

        _ = InitializeAsync(id);
    }

    public TeamListViewModel()
    {
        TeamList = new ObservableCollection<Team>();
        
        PersonName = "empty";
        Number = "0";
        
        TeamList.Add(new Team("Regression", 2, 5));
        TeamList.Add(new Team("OS", 4, 10));
        TeamList.Add(new Team("Regression Team", 7, 20));
        TeamList.Add(new Team("Machine Learning Assignment تيم", 1, 8));
        
    }
    
    private void OnMobileTextReceived(string target, string text)
    {
        if (target == "Name") NameEdit += text;
        else if (target == "Number") NumberEdit += text;
    }

    private void OnMobileBackspacePressed(string target)
    {
        if (target == "Name" && NameEdit.Length > 0) NameEdit = NameEdit[..^1];
        else if (target == "Number" && NumberEdit.Length > 0) NumberEdit = NumberEdit[..^1];
    }
    
    [RelayCommand]
    private void FocusName() => MobileInputHelper.Focus("Name");

    [RelayCommand]
    private void FocusNumber() => MobileInputHelper.Focus("Number", "number");

    private async Task InitializeAsync(int id)
    {
        try
        {
            await GetPersonData(id);

            await GetTeams(id);

            await connection.StartAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task GetTeams(int id)
    {
        try
        {
            TeamList = await client.GetFromJsonAsync($"{BaseURL}/api/v1/team?personId={id}", AppJsonContext.Default.ObservableCollectionTeam);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task GetPersonData(int id)
    {
        var response = await client.GetAsync($"{BaseURL}/api/v1/person/{id}");

        Console.WriteLine(await response.Content.ReadAsStringAsync());

        if (response.IsSuccessStatusCode)
        {
            Person? person = JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(),
                AppJsonContext.Default.Person);

            PersonName = person!.Name;
            Number = person.Number;
            PersonId = (int)person.Id!;
        }
    }

    [RelayCommand]
    private async Task RequestJoin(Team team)
    {
        try
        {
            var response = await client.PostAsync($"{BaseURL}/api/v1/team/request/{team.Id}?personId={PersonId}", null);

            if (response.IsSuccessStatusCode)
            {
            
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [RelayCommand]
    private void Edit()
    {
        NameEdit = "";
        NumberEdit = "";
        ClearErrors();
        IsEditVisible = true;
    }

    [RelayCommand]
    private async Task Save()
    {
        ValidateAllProperties();
        if (!HasErrors)
        {
            var newPerson = new Person { Name = NameEdit, Number = NumberEdit };
            
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newPerson, AppJsonContext.Default.Person),
                    Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{BaseURL}/api/v1/person/edit/{PersonId}", content);
    
                if (response.IsSuccessStatusCode)
                {
                    await GetPersonData(PersonId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Hide();
            }
        }
    }

    [RelayCommand]
    private void Hide()
    {
        IsEditVisible = false;
    }
}