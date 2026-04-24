using System.ComponentModel.DataAnnotations;

namespace TeamSearcherAPI.Models;

public enum Status
{
    NotAccepted,
    Accepted
}

public class TeamPerson
{
    public int TeamId { get; set; }
    public Team Team { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; }

    public Status Status { get; set; }
}