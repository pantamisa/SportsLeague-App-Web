using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly IMapper _mapper;

    public MatchController(IMatchService matchService, IMapper mapper)
    {
        _matchService = matchService;
        _mapper = mapper;
    }

    // GET api/Match/tournament/1
    [HttpGet("tournament/{tournamentId}")]
    public async Task<ActionResult<IEnumerable<MatchResponseDTO>>> GetByTournament(int tournamentId)
    {
        try
        {
            var matches = await _matchService.GetAllByTournamentAsync(tournamentId);
            return Ok(_mapper.Map<IEnumerable<MatchResponseDTO>>(matches));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    // GET api/Match/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MatchResponseDTO>> GetById(int id)
    {
        var match = await _matchService.GetByIdAsync(id);
        if (match == null) return NotFound(new { message = $"Partido con ID {id} no encontrado" });
        return Ok(_mapper.Map<MatchResponseDTO>(match));
    }

    // POST api/Match
    [HttpPost]
    public async Task<ActionResult<MatchResponseDTO>> Create(MatchRequestDTO dto)
    {
        try
        {
            var match = _mapper.Map<Match>(dto);
            var created = await _matchService.CreateAsync(match);
            // Se vuelve a consultar para obtener los nombres de las Navigation Properties
            var matchWithDetails = await _matchService.GetByIdAsync(created.Id);
            var responseDto = _mapper.Map<MatchResponseDTO>(matchWithDetails);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    // PUT api/Match/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, MatchRequestDTO dto)
    {
        try
        {
            var match = _mapper.Map<Match>(dto);
            await _matchService.UpdateAsync(id, match);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    // DELETE api/Match/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _matchService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    // PATCH api/Match/5/status
    [HttpPatch("{id}/status")]
    public async Task<ActionResult> UpdateStatus(int id, UpdateMatchStatusDTO dto)
    {
        try
        {
            await _matchService.UpdateStatusAsync(id, dto.Status);
            return NoContent();
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }
}
