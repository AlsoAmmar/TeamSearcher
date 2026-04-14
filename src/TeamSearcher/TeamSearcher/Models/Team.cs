using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.Models;

public partial class Team : ObservableObject
{
    [ObservableProperty]  private string _teamName;
    [ObservableProperty] private string _currentCount;
    [ObservableProperty]  private string _maxCount;
}