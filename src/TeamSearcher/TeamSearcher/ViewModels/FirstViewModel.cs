using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class FirstViewModel : ViewModelBase
{
    [RelayCommand]
    private void GoToJoin()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new JoinViewModel()));
    }

    [RelayCommand]
    private void GoToCreate()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new CreateViewModel()));
    }
}