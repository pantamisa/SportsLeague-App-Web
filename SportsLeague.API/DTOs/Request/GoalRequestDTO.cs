using SportsLeague.Domain.Enums;

namespace SportsLeague.API.DTOs.Request;

public class GoalRequestDTO
{
    public int PlayerId { get; set; }
    public int Minute { get; set; }
    public GoalType Type { get; set; }  // 0=Normal, 1=Penalty, 2=OwnGoal
}
