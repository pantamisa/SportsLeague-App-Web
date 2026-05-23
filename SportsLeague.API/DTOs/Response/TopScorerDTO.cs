namespace SportsLeague.API.DTOs.Response;

public class TopScorerDTO
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public int Goals { get; set; }              // Total de goles (excluyendo autogoles)
    public int Penalties { get; set; }          // Cuántos de esos goles fueron de penal
    public int MatchesWithGoals { get; set; }   // En cuántos partidos distintos anotó
}
