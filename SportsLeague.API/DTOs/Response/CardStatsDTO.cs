namespace SportsLeague.API.DTOs.Response;

public class CardStatsDTO
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public int YellowCards { get; set; }  // Amarillas acumuladas en el torneo
    public int RedCards { get; set; }     // Rojas acumuladas en el torneo
    public int TotalCards { get; set; }   // Suma de ambas (útil para estadísticas generales)
}
