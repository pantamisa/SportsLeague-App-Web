using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface IGoalRepository : IGenericRepository<Goal>
{
    Task<IEnumerable<Goal>> GetByMatchAsync(int matchId);             // Sin Navigation Properties
    Task<IEnumerable<Goal>> GetByMatchWithDetailsAsync(int matchId);  // Con Player incluido
}
