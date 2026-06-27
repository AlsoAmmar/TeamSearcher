using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Application.DTOs.Team;

public class CreateTeamDto
{
    public string Name { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public Tag Tag { get; set; }
}