using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
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
    private HttpClient client;

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
                try
                {
                    var team = JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(),
                        AppJsonContext.Default.Team);
                
                    ProjectName = team!.Name;
                    CurrentCount = team.CurrentCount;
                    MaxCount = team.MaxCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}