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

public partial class TeamListViewModel : ViewModelBase
{
    [ObservableProperty] private string _personName;
    [ObservableProperty] private string _number;
    [ObservableProperty] private ObservableCollection<Team> _teamList;
    private int PersonId { get; set; }
    HttpClient client;
    private HubConnection connection;

    public TeamListViewModel(int id)
    {
        client = new HttpClient();
        TeamList = new ObservableCollection<Team>();
        
        connection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:5213/personHub?userId={id}")
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

    private async Task InitializeAsync(int id)
    {
        var response = await client.GetAsync($"http://localhost:5213/api/v1/person/{id}");

        Console.WriteLine(await response.Content.ReadAsStringAsync());

        if (response.IsSuccessStatusCode)
        {
            Person? person = JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(),
                AppJsonContext.Default.Person);

            PersonName = person!.Name;
            Number = person.Number;
            PersonId = (int)person.Id!;
        }

        await GetTeams(id);

        await connection.StartAsync();
    }

    private async Task GetTeams(int id)
    {
        TeamList = await client.GetFromJsonAsync($"http://localhost:5213/api/v1/team?personId={id}", AppJsonContext.Default.ObservableCollectionTeam);
    }

    [RelayCommand]
    private async Task RequestJoin(Team team)
    {
        var response = await client.PostAsync($"http://localhost:5213/api/v1/team/request/{team.Id}?personId={PersonId}", null);

        if (response.IsSuccessStatusCode)
        {
            
        }
    }
}