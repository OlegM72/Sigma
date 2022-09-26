namespace GymOffice.Business.DataProviders;
public class GroupTrainingDataProvider : IGroupTrainingDataProvider
{
    private readonly IGroupTrainingRepository _trainingRepository;

    public GroupTrainingDataProvider(IGroupTrainingRepository trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task<IEnumerable<GroupTraining>?> GetAllTrainingsAsync()
    {
        return await _trainingRepository.GetAllAsync();
    }

    public async Task<GroupTraining?> GetTrainingByIdAsync(Guid id)
    {
        GroupTraining? training = await _trainingRepository.GetByIdAsync(id);

        if (training == null)
        {
            throw new NotFoundException(nameof(GroupTraining), id);
        }

        return training;
    }

    public async Task<IEnumerable<GroupTraining>?> GetTrainingsByCoachAsync(Guid coachId)
    {
        return await _trainingRepository.GetTrainingsByCoachAsync(coachId);
    }

    public async Task<IEnumerable<GroupTraining>?> GetTrainingsByDateAsync(DateOnly date)
    {
        return await _trainingRepository.GetTrainingsByDateAsync(date);
    }

    public async Task<IEnumerable<GroupTraining>?> GetTrainingsByDateIntervalAsync(DateOnly startDate, DateOnly endDate)
    {
        return await _trainingRepository.GetTrainingsByDateIntervalAsync(startDate, endDate);
    }

    public async Task<IEnumerable<GroupTraining>?> GetTrainingsByNameAsync(string name)
    {
        return await _trainingRepository.GetTrainingsByNameAsync(name);
    }

    public async Task<IEnumerable<GroupTraining>?> GetTrainingsByTimeIntervalAsync(TimeOnly startTime, TimeOnly endTime)
    {
        return await _trainingRepository.GetTrainingsByTimeIntervalAsync(startTime, endTime);
    }
}
