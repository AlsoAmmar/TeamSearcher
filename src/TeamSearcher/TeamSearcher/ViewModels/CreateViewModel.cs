using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class CreateViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName;
    [ObservableProperty] private int _currentCount = 1;
    [ObservableProperty] private int _maxCount = 5;
    [ObservableProperty] private bool _isMobile;
    [ObservableProperty] private bool _boysOnly;
    [ObservableProperty] private bool _girlsOnly;

    public CreateViewModel()
    {
        if (OperatingSystem.IsBrowser() && MobileInputHelper.IsMobile())
        {
            IsMobile = true;
            MobileInputHelper.TextReceived += (target, text) => 
            {
                if (target == "Project") ProjectName += text;
            };

            MobileInputHelper.BackspacePressed += (target) => 
            {
                if (target == "Project" && ProjectName.Length > 0) 
                    ProjectName = ProjectName[..^1];
            };
        }
    }

    [RelayCommand]
    private void FocusProject() => MobileInputHelper.Focus("Project");

    [RelayCommand]
    private void Search()
    {
        WeakReferenceMessenger.Default.Send(
            new NavigationMessage(new TeamDashViewModel(ProjectName, CurrentCount, MaxCount)));
    }
}