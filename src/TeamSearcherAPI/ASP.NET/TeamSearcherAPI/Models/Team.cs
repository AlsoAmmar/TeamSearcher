using System.ComponentModel.DataAnnotations;

namespace TeamSearcherAPI.Models;

public enum Tag
{
    None,
    BoysOnly,
    GirlsOnly
}

public class Team
{
    [Key]
    public int? Id { get; set; }
    public string Name { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public Tag Tag { get; set; }
    public ICollection<TeamPerson> TeamPersons { get; set; }

}