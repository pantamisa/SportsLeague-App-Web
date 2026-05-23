using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class GoalRepository : GenericRepository<Goal>, IGoalRepository
{
    public GoalRepository(LeagueDbContext context) : base(context) { }

    // Orden cronológico por minuto para que la respuesta sea legible como relato del partido
    public async Task<IEnumerable<Goal>> GetByMatchAsync(int matchId) =>
        await _dbSet
            .Where(g => g.MatchId == matchId)
            .OrderBy(g => g.Minute)
            .ToListAsync();

    // Con Include para obtener el nombre del jugador en el DTO de respuesta
    public async Task<IEnumerable<Goal>> GetByMatchWithDetailsAsync(int matchId) =>
        await _dbSet
            .Where(g => g.MatchId == matchId)
            .Include(g => g.Player)
            .OrderBy(g => g.Minute)
            .ToListAsync();
}
