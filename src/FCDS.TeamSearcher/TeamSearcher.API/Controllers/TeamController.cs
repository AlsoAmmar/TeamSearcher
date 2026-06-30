using Microsoft.AspNetCore.Mvc;
using TeamSearcher.Application.Contracts.Services;
using TeamSearcher.Application.DTOs.Team;

namespace TeamSearcher.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    
    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetRequests(int teamId, CancellationToken cancellationToken)
    {
        var persons = await _teamService.GetPersonRequests(teamId, cancellationToken);
        return Ok(persons);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto team, CancellationToken cancellationToken)
    {
        var teamId = await _teamService.CreateTeamAsync(team, cancellationToken);
        return Ok(teamId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto team, CancellationToken cancellationToken)
    {
        await _teamService.UpdateTeam(team, cancellationToken);
        return Ok();
    }

    [HttpPut("{teamId}/{personId}")]
    public async Task<IActionResult> AcceptRequest(int personId, int teamId, CancellationToken cancellationToken)
    {
        await _teamService.AcceptRequest(personId, teamId, cancellationToken);
        return Ok();
    }
}