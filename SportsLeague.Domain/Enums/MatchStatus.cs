namespace SportsLeague.Domain.Enums;

public enum MatchStatus
{
    Scheduled = 0,    // Partido programado (estado inicial)
    InProgress = 1,   // Partido en curso
    Finished = 2,     // Partido terminado
    Suspended = 3     // Partido suspendido
}
