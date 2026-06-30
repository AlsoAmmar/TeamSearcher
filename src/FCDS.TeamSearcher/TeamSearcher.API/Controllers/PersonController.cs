using Microsoft.AspNetCore.Mvc;
using TeamSearcher.Application.Contracts.Services;
using TeamSearcher.Application.DTOs.Person;

namespace TeamSearcher.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetTeams(int personId, CancellationToken cancellationToken)
    {
        var teams = await _personService.GetAvailableTeams(personId, cancellationToken);
        return Ok(teams);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDto person, CancellationToken cancellationToken)
    {
        var personId = await _personService.CreatePersonAsync(person, cancellationToken);
        return Ok(personId);
    }

    [HttpPut("{personId}")]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonDto person, CancellationToken cancellationToken)
    {
        await _personService.UpdatePerson(person, cancellationToken);
        return Ok();
    }

    [HttpPut("{personId}/{teamId}")]
    public async Task<IActionResult> RequestJoin(int personId, int teamId, CancellationToken cancellationToken)
    {
        await _personService.RequestJoin(personId, teamId, cancellationToken);
        return Ok();
    }
}