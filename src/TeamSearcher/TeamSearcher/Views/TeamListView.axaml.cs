using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TeamSearcher.Models;
using TeamSearcher.ViewModels;

namespace TeamSearcher.Views;

public partial class TeamListView : UserControl
{
    public TeamListView()
    {
        InitializeComponent();
    }

    private async void RequestJoining(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var team = button!.DataContext as Team;
        
        await ((TeamListViewModel)DataContext!).RequestJoinCommand.ExecuteAsync(team);
    }

    private void BorderPress(object? sender, PointerPressedEventArgs e)
    {
        ((TeamListViewModel)DataContext!).HideCommand.Execute(null);
    }
}