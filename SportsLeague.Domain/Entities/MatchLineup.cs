namespace SportsLeague.Domain.Entities;

public class MatchLineup : AuditBase
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }

    // true = Titular (cuenta para el límite de 11 por equipo)
    // false = Suplente (sin límite numérico)
    public bool IsStarter { get; set; }

    // Posición táctica asignada para este partido específico.
    public string Position { get; set; } = string.Empty;

    // Navigation Properties
    public Match  Match  { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
