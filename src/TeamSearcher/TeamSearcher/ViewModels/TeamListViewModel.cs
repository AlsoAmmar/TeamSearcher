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
        PersonName = name;
        Number = number;
    }

    public TeamListViewModel()
    {
        PersonName = "empty";
        Number = "0";
    }
}