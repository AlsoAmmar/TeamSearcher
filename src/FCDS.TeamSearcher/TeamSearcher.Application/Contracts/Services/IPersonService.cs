using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Application.DTOs.Team;

namespace TeamSearcher.Application.Contracts.Services;

public interface IPersonService
{
    Task<int> CreatePersonAsync(CreatePersonDto person, CancellationToken cancellationToken);
    Task<IEnumerable<TeamDto>> GetAvailableTeams(int personId, CancellationToken cancellationToken);
    Task RequestJoin(int personId, int teamId, CancellationToken cancellationToken);
    Task UpdatePerson(UpdatePersonDto person, CancellationToken cancellationToken);
}