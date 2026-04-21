using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class CreateViewModel : ViewModelBase
{
    [ObservableProperty] [Required(ErrorMessage = "يجب إضافة اسم البروجيكت الذي يعمل عليه التيم")] [NotifyDataErrorInfo]
    private string _projectName;
    [ObservableProperty] [Required(ErrorMessage = "لا يمكن ترك الخانة فارغة")] private int? _currentCount = 1;
    [ObservableProperty] [Required(ErrorMessage = "لا يمكن ترك الخانة فارغة")] private int? _maxCount = 5;
    [ObservableProperty] private bool _isMobile;
    [ObservableProperty] private bool _boysOnly;
    [ObservableProperty] private bool _girlsOnly;
    [ObservableProperty] private string _errorMessage;

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
        ErrorMessage = "";
        ValidateAllProperties();
        
        if (!HasErrors)
        {
            WeakReferenceMessenger.Default.Send(
                new NavigationMessage(new TeamDashViewModel(ProjectName, CurrentCount, MaxCount)));
        }else if (MaxCount < CurrentCount)
        {
            ErrorMessage = "رقم الأعضاء الحالي يتعدي العدد الاقصى";
        }else if (MaxCount == CurrentCount && MaxCount != null && CurrentCount != null)
        {
            ErrorMessage = "التيم مكتمل بالفعل";
        }
    }

    [RelayCommand]
    private void RemoveTag()
    {
        BoysOnly = false;
        GirlsOnly = false;
    }
    
    [RelayCommand]
    private void GoBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new FirstViewModel()));
    }
}