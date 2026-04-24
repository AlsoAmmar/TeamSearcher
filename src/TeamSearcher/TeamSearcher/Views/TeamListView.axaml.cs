using Avalonia;
using Avalonia.Controls;
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

    private void RequestJoining(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var team = button!.DataContext as Team;
        
        ((TeamListViewModel)DataContext!).RequestJoinCommand.Execute(team);
    }
}