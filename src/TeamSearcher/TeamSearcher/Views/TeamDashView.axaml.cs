using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TeamSearcher.Models;
using TeamSearcher.ViewModels;

namespace TeamSearcher.Views;

public partial class TeamDashView : UserControl
{
    public TeamDashView()
    {
        InitializeComponent();
    }

    private async void AcceptRequest(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        var person = button!.DataContext as Person;
        
        await ((TeamDashViewModel)DataContext!).AcceptCommand.ExecuteAsync(person);
    }
}