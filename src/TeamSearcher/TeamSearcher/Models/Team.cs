using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamSearcher.Models;

public enum Tag
{
    None,
    BoysOnly,
    GirlsOnly
}

public class Team : ObservableObject
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("currentCount")] public int CurrentCount { get; set; }
    [JsonPropertyName("maxCount")] public int MaxCount { get; set; }
    [JsonPropertyName("tag")] public Tag Tag { get; set; }
    
    [JsonIgnore] public string MergedCount => $"{CurrentCount}/{MaxCount}";
    
    public Team() { }

    public Team(string name, int current, int max)
    {
        Name = name;
        CurrentCount = current;
        MaxCount = max;
    }
}