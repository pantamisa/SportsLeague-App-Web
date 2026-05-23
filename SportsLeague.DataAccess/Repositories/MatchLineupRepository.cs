using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class MatchLineupRepository : GenericRepository<MatchLineup>, IMatchLineupRepository
{
    public MatchLineupRepository(LeagueDbContext context) : base(context) { }

    public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId) =>
        await _dbSet
            .Where(ml => ml.MatchId == matchId)
            .Include(ml => ml.Player)
                .ThenInclude(p => p.Team)  // Necesario para TeamName en el DTO
            // Titulares primero, luego suplentes. Dentro de cada grupo, orden por posición
            .OrderByDescending(ml => ml.IsStarter)
            .ThenBy(ml => ml.Position)
            .ToListAsync();

    public async Task<IEnumerable<MatchLineup>> GetByMatchAndTeamAsync(int matchId, int teamId) =>
        await _dbSet
            .Where(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId)
            .Include(ml => ml.Player)
                .ThenInclude(p => p.Team)
            .OrderByDescending(ml => ml.IsStarter)
            .ThenBy(ml => ml.Position)
            .ToListAsync();

    // AnyAsync es más eficiente que FirstOrDefaultAsync para solo verificar existencia:
    // genera un SELECT TOP 1 en BD en lugar de cargar el objeto completo
    public async Task<bool> ExistsByMatchAndPlayerAsync(int matchId, int playerId) =>
        await _dbSet.AnyAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);

    // CountAsync con filtro de IsStarter = true para la validación V5
    // El filtro por teamId requiere cruzar con Player para obtener su TeamId
    public async Task<int> CountStartersByMatchAndTeamAsync(int matchId, int teamId) =>
        await _dbSet
            .CountAsync(ml => ml.MatchId == matchId
                           && ml.IsStarter == true
                           && ml.Player.TeamId == teamId);
}
