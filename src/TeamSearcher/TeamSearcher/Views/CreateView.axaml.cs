using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace TeamSearcher.Views;

public partial class CreateView : UserControl
{
    public CreateView()
    {
        InitializeComponent();
    }

    private void BorderPressed(object? sender, PointerPressedEventArgs e)
    {
        FocusSinker.Focus();
    }
}