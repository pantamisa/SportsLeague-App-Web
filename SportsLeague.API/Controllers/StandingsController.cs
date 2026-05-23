using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api")]  // Ruta base genérica para URLs tipo /api/standings y /api/stats/...
public class StandingsController : ControllerBase
{
    private readonly IStandingsService _standingsService;

    public StandingsController(IStandingsService standingsService)
    {
        _standingsService = standingsService;
    }

    // GET /api/standings?tournamentId=1
    [HttpGet("standings")]
    public async Task<ActionResult> GetStandings([FromQuery] int tournamentId)
    {
        try
        {
            var standings = await _standingsService.GetStandingsAsync(tournamentId);
            return Ok(standings);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET /api/stats/scorers?tournamentId=1
    [HttpGet("stats/scorers")]
    public async Task<ActionResult> GetTopScorers([FromQuery] int tournamentId)
    {
        try
        {
            var scorers = await _standingsService.GetTopScorersAsync(tournamentId);
            return Ok(scorers);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET /api/stats/cards?tournamentId=1
    [HttpGet("stats/cards")]
    public async Task<ActionResult> GetCardStats([FromQuery] int tournamentId)
    {
        try
        {
            var cards = await _standingsService.GetCardStatsAsync(tournamentId);
            return Ok(cards);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
