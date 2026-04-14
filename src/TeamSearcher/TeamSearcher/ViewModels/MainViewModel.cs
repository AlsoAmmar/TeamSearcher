using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class MainViewModel : ViewModelBase, IRecipient<NavigationMessage>
{
    [ObservableProperty] private ObservableObject _currentPage;

    public MainViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        CurrentPage = new FirstViewModel();
    }

    public void Receive(NavigationMessage message)
    {
        CurrentPage = message.Value;
    }
}