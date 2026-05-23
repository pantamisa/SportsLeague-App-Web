namespace SportsLeague.API.DTOs.Response;

public class MatchLineupDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;  // FirstName + LastName
    public string TeamName { get; set; } = string.Empty;    // Nombre del equipo del jugador
    public bool IsStarter { get; set; }
    public string Position { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
