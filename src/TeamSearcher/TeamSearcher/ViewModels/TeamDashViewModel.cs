using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class TeamDashViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(MergedCount))]
    private int _currentCount;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(MergedCount))]
    private int _maxCount;
    [ObservableProperty] private ObservableCollection<Person> _personList;
    public string MergedCount => $"{CurrentCount}/{MaxCount}";
    private int TeamId { get; set; }
    private HttpClient client;
    private HubConnection connection;

    public TeamDashViewModel()
    {
        PersonList = new ObservableCollection<Person>();
        
        PersonList.Add(new Person{ Name = "احمد" });
        PersonList.Add(new Person{ Name = "محمد" });
        PersonList.Add(new Person{ Name = "محمود" });
        PersonList.Add(new Person{ Name = "مصطفى" });
    }

    public TeamDashViewModel(int id)
    {
        client = new HttpClient();
        PersonList = new ObservableCollection<Person>();

        connection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:5213/teamHub?teamId={id}")
            .WithAutomaticReconnect()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions = AppJsonContext.Default.Options;
            })
            .Build();

        connection.On("GetRequests", async () => await GetPeople(id));

        _ = InitializeAsync(id);
    }

    private async Task InitializeAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"http://localhost:5213/api/v1/team/{id}");
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            
            if (response.IsSuccessStatusCode)
            {
                var team = JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(),
                    AppJsonContext.Default.Team);
            
                ProjectName = team!.Name;
                CurrentCount = team.CurrentCount;
                MaxCount = team.MaxCount;
                TeamId = id;
            }

            await GetPeople(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await connection.StartAsync();
    }

    public async Task GetPeople(int id)
    {
        try
        {
            Console.WriteLine("Getting People");
            PersonList = await client.GetFromJsonAsync($"http://localhost:5213/api/v1/person/requests/{id}", AppJsonContext.Default.ObservableCollectionPerson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [RelayCommand]
    public async Task Accept(Person person)
    {
        var response = await client.PutAsync($"http://localhost:5213/api/v1/person/request/{person.Id}?teamId={TeamId}", null);

        if (response.IsSuccessStatusCode)
        {
            await GetPeople(TeamId);
        }
    }
}