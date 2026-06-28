using TeamSearcher.Application.DTOs.Team;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Application.Mappings;

public static class TeamMapping
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            CurrentCount = team.CurrentCount,
            MaxCount = team.MaxCount,
            Tag = team.Tag 
        };
    }

    public static Team ToEntity(this CreateTeamDto teamDto)
    {
        return new Team
        {
            Name = teamDto.Name,
            CurrentCount = teamDto.CurrentCount,
            MaxCount = teamDto.MaxCount,
            Tag = teamDto.Tag
        };
    }

    public static IEnumerable<TeamDto> ToDtoList(this IEnumerable<Team> teams)
    {
        if (teams == null) return Enumerable.Empty<TeamDto>();

        return teams.Select(t => t.ToDto()).ToList();
    }

    public static void UpdateEntity(this Team team, UpdateTeamDto updatedTeam)
    {
        team.Name = updatedTeam.Name;
        team.CurrentCount = updatedTeam.CurrentCount;
        team.MaxCount = updatedTeam.MaxCount;
        team.Tag = updatedTeam.Tag;
    }
}