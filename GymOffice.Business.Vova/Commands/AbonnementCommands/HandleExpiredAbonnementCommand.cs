namespace GymOffice.Business.Commands.AbonnementCommands;
public class HandleExpiredAbonnementCommand : IHandleExpiredAbonnementCommand
{
    private readonly IAbonnementRepository _abonnementRepository;
    private readonly ICustomerRepository _customerRepository;

    public HandleExpiredAbonnementCommand(IAbonnementRepository abonnementRepository, ICustomerRepository customerRepository)
    {
        _abonnementRepository = abonnementRepository;
        _customerRepository = customerRepository;
    }

    public async Task ExecuteAsync(Abonnement abonnement)
    {
        if (abonnement == null)
            throw new ArgumentNullException(nameof(abonnement));

        await _customerRepository.DeleteAbonnementFromCustomerAsync(abonnement.CustomerId, abonnement);
        await _abonnementRepository.DeleteAsync(abonnement);
    }
}
