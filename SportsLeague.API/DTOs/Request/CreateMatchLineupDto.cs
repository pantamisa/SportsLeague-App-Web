namespace SportsLeague.API.DTOs.Request;

public class CreateMatchLineupDto
{
    public int PlayerId { get; set; }
    public bool IsStarter { get; set; }  // true = Titular, false = Suplente
    public string Position { get; set; } = string.Empty;
}
