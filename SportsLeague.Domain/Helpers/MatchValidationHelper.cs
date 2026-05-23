using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.Domain.Helpers;

public class MatchValidationHelper
{
    private readonly IMatchRepository _matchRepository;
    private readonly IPlayerRepository _playerRepository;

    public MatchValidationHelper(
        IMatchRepository matchRepository,
        IPlayerRepository playerRepository)
    {
        _matchRepository = matchRepository;
        _playerRepository = playerRepository;
    }

    // Validación 1: El partido debe existir y estar en un estado que permita eventos
    // Acepta InProgress (partido en curso) y Finished (para correcciones post-partido)
    public async Task<Match> ValidateMatchForEventAsync(int matchId)
    {
        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match == null)
            throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

        if (match.Status != MatchStatus.InProgress && match.Status != MatchStatus.Finished)
            throw new InvalidOperationException(
                "Solo se pueden registrar eventos en partidos InProgress o Finished");

        return match;  // Retorna el match para que el caller pueda usarlo en la validación siguiente
    }

    // Validación 2: El jugador debe existir y pertenecer a uno de los dos equipos del partido
    // Recibe el Match ya cargado (lo obtiene de ValidateMatchForEventAsync) para evitar una query extra
    public async Task<Player> ValidatePlayerInMatchAsync(int playerId, Match match)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player == null)
            throw new KeyNotFoundException($"No se encontró el jugador con ID {playerId}");

        if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
            throw new InvalidOperationException(
                "El jugador no pertenece a ninguno de los equipos del partido");

        return player;
    }

    // Validación 3: El minuto es una validación de datos pura (sin BD), por eso es estática
    // No depende de ningún repositorio — solo verifica un rango numérico
    public static void ValidateMinute(int minute)
    {
        if (minute < 1 || minute > 120)
            throw new InvalidOperationException("El minuto debe estar entre 1 y 120");
    }
}
