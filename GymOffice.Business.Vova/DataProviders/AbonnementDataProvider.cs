using GymOffice.Common.Utilities.Enums;

namespace GymOffice.Business.DataProviders;
public class AbonnementDataProvider : IAbonnementDataProvider
{
    private readonly IAbonnementRepository _abonnementRepository;

    public AbonnementDataProvider(IAbonnementRepository abonnementRepository)
    {
        _abonnementRepository = abonnementRepository;
    }

    public async Task<Abonnement?> GetAbonnementByIdAsync(Guid id)
    {
        Abonnement? abonnement = await _abonnementRepository.GetByIdAsync(id);

        if (abonnement == null || abonnement.Id != id)
        {
            throw new NotFoundException(nameof(Abonnement), id);
        }

        return abonnement;
    }

    public async Task<IEnumerable<Abonnement>?> GetAbonnementsByCustomerIdAsync(Guid customerId)
    {
        return await _abonnementRepository.GetByCustomerIdAsync(customerId);        
    }

    public async Task<IEnumerable<Abonnement>?> GetAbonnementsByDurationAsync(AbonnementDuration duration)
    {
        return await _abonnementRepository.GetByDurationAsync(duration);
    }

    public async Task<IEnumerable<Abonnement>?> GetAbonnementsByTypeAsync(AbonnementType type)
    {
        return await _abonnementRepository.GetByTypeAsync(type);
    }

    public async Task<IEnumerable<Abonnement>?> GetAllAbonnementsAsync()
    {
        return await _abonnementRepository.GetAllAsync();
    }
}
