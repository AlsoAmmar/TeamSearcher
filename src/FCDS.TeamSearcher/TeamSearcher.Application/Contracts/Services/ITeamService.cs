using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Application.DTOs.Team;

namespace TeamSearcher.Application.Contracts.Services;

public interface ITeamService
{
    Task<int> CreateTeamAsync(CreateTeamDto team, CancellationToken cancellationToken);
    Task<IEnumerable<PersonDto>> GetPersonRequests(int teamId, CancellationToken cancellationToken);
    Task AcceptRequest(int personId, int teamId, CancellationToken cancellationToken);
    Task UpdateTeam(UpdateTeamDto team, CancellationToken cancellationToken);
}