namespace GymOffice.Business.DataProviders;
public class AbonnementTypeDataProvider : IAbonnementTypeDataProvider
{
    private readonly IAbonnementTypeRepository _abonnementTypeRepository;

    public AbonnementTypeDataProvider(IAbonnementTypeRepository abonnementTypeRepository)
    {
        _abonnementTypeRepository = abonnementTypeRepository;
    }

    public async Task<AbonnementType?> GetAbonnementTypeByIdAsync(Guid id)
    {
        AbonnementType? entity = await _abonnementTypeRepository.GetByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(AbonnementType), id);
        }

        return entity;
    }

    public async Task<AbonnementType?> GetAbonnementTypeByNameAsync(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            throw new ArgumentNullException(nameof(typeName), "Invalid abonnement type name");
        }

        AbonnementType? entity = await _abonnementTypeRepository.GetByNameAsync(typeName);

        if (entity == null || entity.Name != typeName)
        {
            throw new NotFoundException(nameof(AbonnementType), typeName);
        }

        return entity;
    }

    public async Task<IEnumerable<AbonnementType>?> GetAllAbonnementTypesAsync()
    {
        return await _abonnementTypeRepository.GetAllTypesAsync();
    }
}
