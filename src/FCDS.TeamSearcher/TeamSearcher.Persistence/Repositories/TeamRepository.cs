using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    public Task<Team> GetAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Team>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Team> AddAsync(Team entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Team entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Team entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Team>> GetAvailableTeamsAsync(int personId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RequestAcceptedExistsAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AcceptRequestAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}