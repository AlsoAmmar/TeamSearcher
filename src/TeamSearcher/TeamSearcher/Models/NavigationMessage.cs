using CommunityToolkit.Mvvm.Messaging.Messages;
using TeamSearcher.ViewModels;

namespace TeamSearcher.Models;

public class NavigationMessage : ValueChangedMessage<ViewModelBase>
{
    public NavigationMessage(ViewModelBase viewModel) : base(viewModel) { }
}