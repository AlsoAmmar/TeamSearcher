using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.Models;

public partial class Person : ObservableObject
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("number")] public string Number { get; set; }
    
    public Person() { }
}