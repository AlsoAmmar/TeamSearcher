using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.ViewModels;

public partial class TeamDashViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(MergedCount))]
    private int _currentCount;
    [ObservableProperty] private int _maxCount;
    public string MergedCount => $"{CurrentCount}/{MaxCount}";

    public TeamDashViewModel() { }

    public TeamDashViewModel(string name, int currentCount, int maxCount)
    {
        ProjectName = name;
        CurrentCount = currentCount;
        MaxCount = maxCount;
    }
}