using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Application.DTOs.Team;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public Tag Tag { get; set; }
}