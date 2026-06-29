using Microsoft.EntityFrameworkCore;
using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Domain.Entities;
using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Persistence.Repositories;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    private readonly AppDbContext _db;
    
    public PersonRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Person>> GetPersonRequestsAsync(int teamId, CancellationToken cancellationToken)
    {
        var persons = await _db.TeamPersons
            .Where(t => t.TeamId == teamId && t.Status == Status.NotAccepted)
            .Select(t => t.Person)
            .ToListAsync(cancellationToken);

        return persons;
    }

    public async Task<bool> RequestExistsAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        return await _db.TeamPersons.AnyAsync(t => t.PersonId == personId && t.TeamId == teamId, cancellationToken);
    }

    public async Task RequestJoinAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        _db.TeamPersons.Add(new TeamPerson{ TeamId = teamId, PersonId = personId, Status = Status.NotAccepted });
        await _db.SaveChangesAsync();
    }
}