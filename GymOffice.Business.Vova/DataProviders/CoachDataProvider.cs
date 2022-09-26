namespace GymOffice.Business.DataProviders;
public class CoachDataProvider : ICoachDataProvider
{
    private readonly ICoachRepository _coachRepository;

    public CoachDataProvider(ICoachRepository coachRepository)
    {
        _coachRepository = coachRepository;
    }

    public async Task<IEnumerable<Coach>?> GetActiveCoachesAsync()
    {
        return await _coachRepository.GetActiveCoachesAsync();
    }

    public async Task<IEnumerable<Coach>?> GetAllCoachesAsync()
    {
        return await _coachRepository.GetAllCoachesAsync();
    }

    public async Task<Coach?> GetCoachByIdAsync(Guid id)
    {        
        Coach? entity = await _coachRepository.GetByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(Coach), id);
        }

        return entity;
    }

    public async Task<IEnumerable<Coach>?> SearchCoachesAsync(CoachSearchOptions options)
    {
        return await _coachRepository.SearchCoachesAsync(options);
    }
}
