using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class JoinViewModel : ViewModelBase
{
    [ObservableProperty] [Required(ErrorMessage = "يجب إضافة اسم")] [NotifyDataErrorInfo]
    private string _name = string.Empty;
    [ObservableProperty] [Required(ErrorMessage = "مطلوب رقم الواتساب لكي يتواصل معك مالك التيم")] [PhoneNumber(ErrorMessage = "رقم الهاتف غير صحيح")] [NotifyDataErrorInfo]
    private string _number = string.Empty;
    [ObservableProperty]
    private bool _isMobile;

    public JoinViewModel()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
        {
            IsMobile = MobileInputHelper.IsMobile();

            if (IsMobile)
            {
                MobileInputHelper.TextReceived += OnMobileTextReceived;
                MobileInputHelper.BackspacePressed += OnMobileBackspacePressed;
            }
        }
    }

    private void OnMobileTextReceived(string target, string text)
    {
        if (target == "Name") Name += text;
        else if (target == "Number") Number += text;
    }

    private void OnMobileBackspacePressed(string target)
    {
        if (target == "Name" && Name.Length > 0) Name = Name[..^1];
        else if (target == "Number" && Number.Length > 0) Number = Number[..^1];
    }

    // 3. Commands to trigger the keyboard
    [RelayCommand]
    private void FocusName() => MobileInputHelper.Focus("Name");

    [RelayCommand]
    private void FocusNumber() => MobileInputHelper.Focus("Number", "number");

    [RelayCommand]
    private void Search()
    {
        ValidateAllProperties();
        if (!HasErrors)
        {
            WeakReferenceMessenger.Default.Send(new NavigationMessage(new TeamListViewModel(Name, Number)));
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new FirstViewModel()));
    }
}