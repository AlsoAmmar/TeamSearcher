using Microsoft.AspNetCore.Mvc;
using TeamSearcher.Application.Contracts.Services;

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
    
    
}