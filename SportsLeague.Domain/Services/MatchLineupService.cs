using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Helpers;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class MatchLineupService : IMatchLineupService
{
    private readonly IMatchLineupRepository _lineupRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly MatchValidationHelper _validationHelper;
    private readonly ILogger<MatchLineupService> _logger;

    public MatchLineupService(
        IMatchLineupRepository lineupRepository,
        IMatchRepository matchRepository,
        MatchValidationHelper validationHelper,
        ILogger<MatchLineupService> logger)
    {
        _lineupRepository  = lineupRepository;
        _matchRepository   = matchRepository;
        _validationHelper  = validationHelper;
        _logger            = logger;
    }

    public async Task<MatchLineup> AddPlayerAsync(int matchId, MatchLineup lineup)
    {
        // V6 — El partido debe existir y estar en estado Scheduled.
        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException(
                $"No se encontró el partido con ID {matchId}");

        if (match.Status != MatchStatus.Scheduled)
            throw new InvalidOperationException(
                "Solo se pueden registrar alineaciones en partidos Scheduled");

        // V2 y V3 — El jugador debe existir y pertenecer a uno de los dos equipos.
        var player = await _validationHelper.ValidatePlayerInMatchAsync(lineup.PlayerId, match);

        // V4 — El jugador no puede estar registrado dos veces en la misma alineación.
        var alreadyExists = await _lineupRepository
            .ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId);
        if (alreadyExists)
            throw new InvalidOperationException(
                "El jugador ya está registrado en la alineación de este partido");

        // V5 — Máximo 11 titulares por equipo por partido.
        if (lineup.IsStarter)
        {
            var startersCount = await _lineupRepository
                .CountStartersByMatchAndTeamAsync(matchId, player.TeamId);

            if (startersCount >= 11)
                throw new InvalidOperationException(
                    "El equipo ya tiene 11 titulares registrados en este partido");
        }

        // Asignar la FK desde los datos validados, no desde el body del request
        lineup.MatchId  = matchId;
        lineup.PlayerId = player.Id;

        _logger.LogInformation(
            "Adding player {PlayerId} to lineup of match {MatchId} as {Role}",
            lineup.PlayerId, matchId, lineup.IsStarter ? "Starter" : "Substitute");

        return await _lineupRepository.CreateAsync(lineup);
    }

    public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
    {
        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException(
                $"No se encontró el partido con ID {matchId}");

        return await _lineupRepository.GetByMatchAsync(matchId);
    }

    public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId)
    {
        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException(
                $"No se encontró el partido con ID {matchId}");

        if (match.HomeTeamId != teamId && match.AwayTeamId != teamId)
            throw new InvalidOperationException(
                "El equipo no participa en este partido");

        return await _lineupRepository.GetByMatchAndTeamAsync(matchId, teamId);
    }

    public async Task DeleteAsync(int matchId, int lineupId)
    {
        var lineup = await _lineupRepository.GetByIdAsync(lineupId);
        if (lineup == null)
            throw new KeyNotFoundException(
                $"No se encontró el registro de alineación con ID {lineupId}");

        if (lineup.MatchId != matchId)
            throw new InvalidOperationException(
                "El registro de alineación no pertenece a este partido");

        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match!.Status != MatchStatus.Scheduled)
            throw new InvalidOperationException(
                "Solo se pueden modificar alineaciones en partidos Scheduled");

        _logger.LogInformation(
            "Removing player {PlayerId} from lineup of match {MatchId}",
            lineup.PlayerId, matchId);

        await _lineupRepository.DeleteAsync(lineupId);
    }
}
