using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    public Task<Person> GetAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Person> AddAsync(Person entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Person entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Person entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetPersonRequestsAsync(int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RequestExistsAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RequestJoinAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}