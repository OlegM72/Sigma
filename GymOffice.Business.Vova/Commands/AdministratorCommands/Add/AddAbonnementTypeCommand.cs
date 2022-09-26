namespace GymOffice.Business.Commands.AdministratorCommands.Add;
public class AddAbonnementTypeCommand : IAddAbonnementTypeCommand
{
    private readonly IAbonnementTypeRepository _typeRepository;

    public AddAbonnementTypeCommand(IAbonnementTypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<Guid> ExecuteAsync(AbonnementType abonnementType)
    {
        if (abonnementType == null)
        {
            throw new ArgumentNullException(nameof(abonnementType));
        }
        if (abonnementType.StartVisitTime > abonnementType.EndVisitTime)
        {
            throw new ArgumentException("Start time cannot be greater than end visit time");
        }
        if (await _typeRepository.GetByIdAsync(abonnementType.Id) != null)
        {
            throw new SameEntityExistsException(nameof(AbonnementType), abonnementType.Id);
        }
        if (await _typeRepository.GetByNameAsync(abonnementType.Name) != null)
        {
            throw new SameEntityExistsException(nameof(AbonnementType), abonnementType.Name);
        }

        return await _typeRepository.AddTypeAsync(abonnementType);
    }
}
