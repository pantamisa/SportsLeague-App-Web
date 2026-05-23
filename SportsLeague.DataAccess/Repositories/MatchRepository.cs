using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class MatchRepository : GenericRepository<Match>, IMatchRepository
{
    public MatchRepository(LeagueDbContext context) : base(context) { }

    // Ordena por jornada y luego por fecha
    public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId) =>
        await _dbSet
            .Where(m => m.TournamentId == tournamentId)
            .OrderBy(m => m.Matchday).ThenBy(m => m.MatchDate)
            .ToListAsync();

    // Un equipo puede ser local O visitante en cualquier partido
    public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId) =>
        await _dbSet
            .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .OrderBy(m => m.MatchDate)
            .ToListAsync();

    // Carga los 4 relacionados en un solo SELECT con 4 JOINs
    public async Task<Match?> GetByIdWithDetailsAsync(int id) =>
        await _dbSet
            .Include(m => m.Tournament)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Referee)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Match>> GetByTournamentWithDetailsAsync(int tournamentId) =>
        await _dbSet
            .Where(m => m.TournamentId == tournamentId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Referee)
            .OrderBy(m => m.Matchday).ThenBy(m => m.MatchDate)
            .ToListAsync();
}
