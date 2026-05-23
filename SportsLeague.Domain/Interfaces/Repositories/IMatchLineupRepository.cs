using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface IMatchLineupRepository : IGenericRepository<MatchLineup>
{
    // Toda la alineación del partido, con datos del jugador incluidos
    Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId);

    // Alineación filtrada por equipo (para el endpoint GET /lineup/team/{teamId})
    Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId);

    // Verifica si el jugador ya está en la alineación (validación V4)
    Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId);

    // Cuenta titulares de un equipo en un partido (validación V5 — límite de 11)
    Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId);
}
