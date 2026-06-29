using Microsoft.EntityFrameworkCore;
using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Domain.Entities;
using TeamSearcher.Domain.Enums;

namespace TeamSearcher.Persistence.Repositories;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    private readonly AppDbContext _db;
    
    public TeamRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Team>> GetAvailableTeamsAsync(int personId, CancellationToken cancellationToken)
    {
        var teams = await _db.Team
            .Where(t => !_db.TeamPersons.Any(tp => 
                tp.TeamId == t.Id && 
                tp.PersonId == personId && 
                tp.Status == Status.Accepted))
            .ToListAsync();

        return teams.Where(t => t.CurrentCount < t.MaxCount);
    }

    public async Task<bool> RequestAcceptedExistsAsync(int personId, int teamId, CancellationToken cancellationToken)
    { 
        return await _db.TeamPersons.AnyAsync(tp => tp.TeamId == teamId && tp.PersonId == personId && tp.Status == Status.Accepted);
    }

    public async Task AcceptRequestAsync(int personId, int teamId, CancellationToken cancellationToken)
    {
        var teamPerson = await _db.TeamPersons.FindAsync(personId, teamId);
        var team = await _db.Team.FindAsync(teamId);
        
        teamPerson!.Status = Status.Accepted;
        team!.CurrentCount++;
        
        await _db.SaveChangesAsync(cancellationToken);
    }
}