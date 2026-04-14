using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.ViewModels;

public partial class TeamDashViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName;
    [ObservableProperty] private int _currentCount;
    [ObservableProperty] private int _maxCount;
    [ObservableProperty] private string _mergedCount;

    public TeamDashViewModel() { }

    public TeamDashViewModel(string name, int currentCount, int maxCount)
    {
        ProjectName = name;
        CurrentCount = currentCount;
        MaxCount = maxCount;
        MergedCount = $"{CurrentCount}/{MaxCount}";
    }
}