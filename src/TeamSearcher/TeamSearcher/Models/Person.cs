using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.Models;

public partial class Person : ObservableObject
{
    [ObservableProperty] private string _name;

    public Person(string name)
    {
        Name = name;
    }
}