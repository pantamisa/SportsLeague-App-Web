using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class MatchResultRepository : GenericRepository<MatchResult>, IMatchResultRepository
{
    public MatchResultRepository(LeagueDbContext context) : base(context) { }

    // Se busca por MatchId porque es la FK única, no por el Id propio del resultado
    public async Task<MatchResult?> GetByMatchIdAsync(int matchId) =>
        await _dbSet.Where(mr => mr.MatchId == matchId).FirstOrDefaultAsync();
}
