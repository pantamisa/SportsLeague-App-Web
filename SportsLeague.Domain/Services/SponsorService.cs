using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace SportsLeague.Domain.Services;

public class SponsorService : ISponsorService {
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
    private readonly ILogger<SponsorService> _logger;

    public SponsorService(
        ISponsorRepository sponsorRepository,
        ITournamentRepository tournamentRepository,
        ITournamentSponsorRepository tournamentSponsorRepository,
        ILogger<SponsorService> logger)
    {
        _sponsorRepository = sponsorRepository;
        _tournamentRepository = tournamentRepository;
        _tournamentSponsorRepository = tournamentSponsorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Sponsor>> GetAllAsync() => await _sponsorRepository.GetAllAsync();

    public async Task<Sponsor?> GetByIdAsync(int id) => await _sponsorRepository.GetByIdAsync(id);

    public async Task<Sponsor> CreateAsync(Sponsor sponsor) {
  
        if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
            throw new InvalidOperationException($"Ya existe un patrocinador con el nombre '{sponsor.Name}'");

        if (!IsValidEmail(sponsor.ContactEmail))
            throw new InvalidOperationException($"El email '{sponsor.ContactEmail}' no tiene un formato válido");

        return await _sponsorRepository.CreateAsync(sponsor);
    }

    public async Task UpdateAsync(int id, Sponsor sponsor) {
        var existing = await _sponsorRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");

        if (existing.Name != sponsor.Name && await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
            throw new InvalidOperationException($"Ya existe un patrocinador con el nombre '{sponsor.Name}'");

        if (!IsValidEmail(sponsor.ContactEmail))
            throw new InvalidOperationException($"El email '{sponsor.ContactEmail}' no tiene un formato válido");

        existing.Name = sponsor.Name;
        existing.ContactEmail = sponsor.ContactEmail;
        existing.Phone = sponsor.Phone;
        existing.WebsiteUrl = sponsor.WebsiteUrl;
        existing.Category = sponsor.Category;

        await _sponsorRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) {
        if (!await _sponsorRepository.ExistsAsync(id))
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");
        await _sponsorRepository.DeleteAsync(id);
    }

    public async Task<TournamentSponsor> LinkSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount) {

        var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
        if (sponsor == null)
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
        if (tournament == null)
            throw new KeyNotFoundException($"No se encontró el torneo con ID {tournamentId}");


        if (contractAmount <= 0)
            throw new InvalidOperationException("El monto del contrato debe ser mayor a 0");

        var existing = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (existing != null)
            throw new InvalidOperationException("Este patrocinador ya está vinculado a este torneo");

        var tournamentSponsor = new TournamentSponsor
        {
            SponsorId = sponsorId,
            TournamentId = tournamentId,
            ContractAmount = contractAmount,
            JoinedAt = DateTime.UtcNow
        };

        return await _tournamentSponsorRepository.CreateAsync(tournamentSponsor);
    }

    public async Task UnlinkSponsorFromTournamentAsync(int sponsorId, int tournamentId) {
        var link = await _tournamentSponsorRepository.GetByTournamentAndSponsorAsync(tournamentId, sponsorId);
        if (link == null)
            throw new KeyNotFoundException("No se encontró la vinculación entre el patrocinador y el torneo");

        await _tournamentSponsorRepository.DeleteAsync(link.Id);
    }

    public async Task<IEnumerable<TournamentSponsor>> GetTournamentsBySponsorAsync(int sponsorId) {
        if (!await _sponsorRepository.ExistsAsync(sponsorId))
            throw new KeyNotFoundException($"No se encontró el patrocinador con ID {sponsorId}");

        return await _tournamentSponsorRepository.GetBySponsorAsync(sponsorId);
    }

    private bool IsValidEmail(string email) {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}