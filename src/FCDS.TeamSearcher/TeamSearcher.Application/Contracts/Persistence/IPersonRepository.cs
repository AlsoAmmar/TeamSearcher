using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Application.Contracts.Persistence;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetPersonRequestsAsync(int teamId, CancellationToken cancellationToken);
    Task<bool> RequestExistsAsync(int personId, int teamId, CancellationToken cancellationToken);
    Task RequestJoinAsync(int personId, int teamId, CancellationToken cancellationToken);
}