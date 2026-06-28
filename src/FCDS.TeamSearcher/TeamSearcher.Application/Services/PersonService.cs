using TeamSearcher.Application.Contracts.Persistence;
using TeamSearcher.Application.Contracts.Services;
using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Application.DTOs.Team;
using TeamSearcher.Application.Mappings;

namespace TeamSearcher.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeamRepository _teamRepository;

    public PersonService(IPersonRepository personRepository, ITeamRepository teamRepository)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
    }
    
    public async Task<int> CreatePersonAsync(CreatePersonDto personDto, CancellationToken cancellationToken)
    {
        var person = personDto.ToEntity();
        await _personRepository.AddAsync(person, cancellationToken);
        
        return person.Id;
    }

    public async Task<IEnumerable<TeamDto>> GetAvailableTeams(int personId, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.GetAvailableTeamsAsync(personId, cancellationToken);

        return teams.ToDtoList();
    }

    public async Task RequestJoin(int personId, int teamId, CancellationToken cancellationToken)
    {
        var exists = await _personRepository.RequestExistsAsync(personId, teamId, cancellationToken);
        
        if (exists) throw new Exception("Request already exists");
        
        await _personRepository.RequestJoinAsync(personId, teamId, cancellationToken);
    }

    public async Task UpdatePerson(UpdatePersonDto updatePersonDto, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetAsync(updatePersonDto.Id, cancellationToken);
        person.UpdateEntity(updatePersonDto);
        
        await _personRepository.UpdateAsync(person, cancellationToken);
    }
}