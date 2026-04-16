using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class TeamDashViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(MergedCount))]
    private int _currentCount;
    [ObservableProperty] private int _maxCount;
    [ObservableProperty] private ObservableCollection<Person> _personList;
    public string MergedCount => $"{CurrentCount}/{MaxCount}";

    public TeamDashViewModel()
    {
        PersonList = new ObservableCollection<Person>();
        
        PersonList.Add(new Person("احمد"));
        PersonList.Add(new Person("محمد"));
        PersonList.Add(new Person("محمود"));
        PersonList.Add(new Person("مصطفى"));
    }

    public TeamDashViewModel(string name, int currentCount, int maxCount)
    {
        PersonList = new ObservableCollection<Person>();
        
        ProjectName = name;
        CurrentCount = currentCount;
        MaxCount = maxCount;
    }
}