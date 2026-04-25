using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TeamSearcher.Models;

[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(Team))]
[JsonSerializable(typeof(ObservableCollection<Team>))]
[JsonSerializable(typeof(ObservableCollection<Person>))]
public partial class AppJsonContext : JsonSerializerContext
{
    
}