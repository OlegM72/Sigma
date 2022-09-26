namespace GymOffice.Business.Commands.EmployeeCommands.Add;
public class AddAbonnementCommand : IAddAbonnementCommand
{
    private readonly IAbonnementRepository _abonnementRepository;
    private readonly ICustomerRepository _customerRepository;

    public AddAbonnementCommand(IAbonnementRepository abonnementRepository, ICustomerRepository customerRepository)
    {
        _abonnementRepository = abonnementRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Guid> ExecuteAsync(Abonnement abonnement)
    {
        if (abonnement == null)
        {
            throw new ArgumentNullException(nameof(abonnement));
        }
        if (await _abonnementRepository.GetByIdAsync(abonnement.Id) != null)
        {
            throw new SameEntityExistsException(nameof(Abonnement), abonnement.Id);
        }
        if (await _customerRepository.GetByIdAsync(abonnement.CustomerId) == null)
        {
            throw new NotFoundException(nameof(Customer), abonnement.CustomerId);
        }

        if (abonnement.IssueTime == default || abonnement.IssueTime > DateTime.Now)
        {
            abonnement.IssueTime = DateTime.Now;
        }

        await _customerRepository.AddAbonnementToCustomerAsync(abonnement, abonnement.CustomerId);

        return await _abonnementRepository.AddAbonnementAsync(abonnement);
    }
}
