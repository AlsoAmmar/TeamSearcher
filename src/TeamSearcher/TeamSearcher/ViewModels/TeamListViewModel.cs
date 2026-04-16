using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class TeamListViewModel : ViewModelBase
{
    [ObservableProperty] private string _personName;
    [ObservableProperty] private string _number;
    [ObservableProperty] private ObservableCollection<Team> _teamList;

    public TeamListViewModel(string name, string number)
    {
        TeamList = new ObservableCollection<Team>();
        
        PersonName = name;
        Number = number;
    }

    public TeamListViewModel()
    {
        TeamList = new ObservableCollection<Team>();
        
        PersonName = "empty";
        Number = "0";
        
        TeamList.Add(new Team("Regression", "2", "5"));
        TeamList.Add(new Team("OS", "4", "10"));
        TeamList.Add(new Team("Regression Team", "7", "20"));
        TeamList.Add(new Team("Machine Learning Assignment تيم", "1", "8"));
        
    }
}