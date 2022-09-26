namespace GymOffice.Business.Commands.AbonnementCommands;
public class ActivateAbonnementCommand : IActivateAbonnementCommand
{
    private readonly IAbonnementRepository _abonnementRepository;

    public ActivateAbonnementCommand(IAbonnementRepository abonnementRepository)
    {
        _abonnementRepository = abonnementRepository;
    }

    public async Task ExecuteAsync(Abonnement abonnement)
    {
        if (abonnement == null)
            throw new ArgumentNullException(nameof(abonnement));

        abonnement.ActivationTime = DateTime.Now;

        await _abonnementRepository.UpdateAsync(abonnement);
    }
}
