using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.Models;

public partial class Team : ObservableObject
{
    [ObservableProperty]  private string _teamName;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(MergedCount))]
    private string _currentCount;
    [ObservableProperty]  private string _maxCount;
    
    public string MergedCount => $"{CurrentCount}/{MaxCount}";

    public Team(string name, string current, string max)
    {
        TeamName = name;
        CurrentCount = current;
        MaxCount = max;
    }
}