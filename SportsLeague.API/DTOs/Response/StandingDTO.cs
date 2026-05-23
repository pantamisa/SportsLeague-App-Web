namespace SportsLeague.API.DTOs.Response;

public class StandingDTO
{
    public int Position { get; set; }          // Posición en la tabla (1 = líder)
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int MatchesPlayed { get; set; }     // PJ — Partidos Jugados
    public int Wins { get; set; }              // PG — Partidos Ganados
    public int Draws { get; set; }             // PE — Partidos Empatados
    public int Losses { get; set; }            // PP — Partidos Perdidos
    public int GoalsFor { get; set; }          // GF — Goles a Favor
    public int GoalsAgainst { get; set; }      // GC — Goles en Contra
    public int GoalDifference { get; set; }    // DG = GF - GC
    public int Points { get; set; }            // Pts = (PG × 3) + PE
}
