using System.ComponentModel.DataAnnotations;
using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Domain.Entities;

public class TeamPerson
{
    public int TeamId { get; set; }
    public Team Team { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; }

    public Status Status { get; set; }
}