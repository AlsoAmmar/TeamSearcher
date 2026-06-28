using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Application.Contracts.Persistence;

public interface ITeamRepository : IGenericRepository<Team>
{
    Task<IEnumerable<Team>> GetAvailableTeamsAsync(int personId, CancellationToken cancellationToken);
    Task<bool> RequestAcceptedExistsAsync(int personId, int teamId, CancellationToken cancellationToken);
    Task AcceptRequestAsync(int personId, int teamId, CancellationToken cancellationToken);
}