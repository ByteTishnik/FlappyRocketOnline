using Microsoft.AspNetCore.Mvc;
using FlappyRocket.Backend.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlappyRocket.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly LeaderboardService _leaderboardService;

    public LeaderboardController(LeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public IActionResult GetScores(Difficulty difficulty)
    {
        return Ok(_leaderboardService.GetScores(difficulty));
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddScore(AddScoreRequest request)
    {
        
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if(claim == null)
        {
            return Unauthorized();
        }

        int userId = int.Parse(claim.Value);


        _leaderboardService.AddScore(request , userId);

        return Ok("Score added successesfully");
    }
}