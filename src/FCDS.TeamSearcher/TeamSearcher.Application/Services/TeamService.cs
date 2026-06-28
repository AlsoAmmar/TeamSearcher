using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Application.Contracts.Services;
using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Application.DTOs.Team;
using TeamSearcher.Application.Mappings;

namespace TeamSearcher.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPersonRepository _personRepository;

    public TeamService(ITeamRepository teamRepository, IPersonRepository personRepository)
    {
        _teamRepository = teamRepository;
        _personRepository = personRepository;
    }
    
    public async Task<int> CreateTeamAsync(CreateTeamDto teamDto, CancellationToken cancellationToken)
    {
        var team = teamDto.ToEntity();
        await _teamRepository.AddAsync(team, cancellationToken);
        
        return team.Id;
    }

    public async Task<IEnumerable<PersonDto>> GetPersonRequests(int teamId, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetPersonRequestsAsync(teamId, cancellationToken);

        return persons.ToDtoList();
    }

    public async Task AcceptRequest(int personId, int teamId, CancellationToken cancellationToken)
    {
        var accepted = await _teamRepository.RequestAcceptedExistsAsync(personId, teamId, cancellationToken);
        
        if (accepted) throw new Exception("Request already accepted");
        
        await _teamRepository.AcceptRequestAsync(personId, teamId, cancellationToken);
    }

    public async Task UpdateTeam(UpdateTeamDto updateTeamDto, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetAsync(updateTeamDto.Id, cancellationToken);
        team.UpdateEntity(updateTeamDto);
        
        await _teamRepository.UpdateAsync(team, cancellationToken);
    }
}