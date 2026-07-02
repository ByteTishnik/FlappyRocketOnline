using Microsoft.AspNetCore.Mvc;
using FlappyRocket.Backend.Controllers;
using Microsoft.AspNetCore.Authorization;

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
    public IActionResult GetScores()
    {
        return Ok(_leaderboardService.GetScores());
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddScore([FromBody] ScoreEntry score)
    {
        _leaderboardService.AddScore(score);

        return Ok("Score added successesfully");
    }
}