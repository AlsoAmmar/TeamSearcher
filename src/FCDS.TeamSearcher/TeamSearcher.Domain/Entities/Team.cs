using System.ComponentModel.DataAnnotations;
using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public Tag Tag { get; set; }
    public ICollection<TeamPerson> TeamPersons { get; set; }

}