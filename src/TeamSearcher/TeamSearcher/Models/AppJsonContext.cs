using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TeamSearcher.Models;

[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(Team))]
[JsonSerializable(typeof(ObservableCollection<Team>))]
public partial class AppJsonContext : JsonSerializerContext
{
    
}