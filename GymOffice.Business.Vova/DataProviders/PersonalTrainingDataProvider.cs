namespace GymOffice.Business.DataProviders;
public class PersonalTrainingDataProvider : IPersonalTrainingDataProvider
{
    private readonly IPersonalTrainingRepository _trainingRepository;

    public PersonalTrainingDataProvider(IPersonalTrainingRepository trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task<IEnumerable<PersonalTraining>?> GetAllTrainingsAsync()
    {
        return await _trainingRepository.GetAllAsync();
    }

    public async Task<PersonalTraining?> GetTrainingByIdAsync(Guid id)
    {
        return await _trainingRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<PersonalTraining>?> GetTrainingsByCoachAsync(Guid coachId)
    {
        return await _trainingRepository.GetTrainingsByCoachAsync(coachId);
    }

    public async Task<IEnumerable<PersonalTraining>?> GetTrainingsByCustomerAsync(Guid customerId)
    {
        return await _trainingRepository.GetTrainingsByCustomerAsync(customerId);
    }

    public async Task<IEnumerable<PersonalTraining>?> GetTrainingsByDateAsync(DateOnly date)
    {
        return await _trainingRepository.GetTrainingsByDateAsync(date);
    }

    public async Task<IEnumerable<PersonalTraining>?> GetTrainingsByDateIntervalAsync(DateOnly startDate, DateOnly endDate)
    {
        return await _trainingRepository.GetTrainingsByDateIntervalAsync(startDate, endDate);
    }

    public async Task<IEnumerable<PersonalTraining>?> GetTrainingsByTimeIntervalAsync(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime > endTime)
        {
            throw new ArgumentException("Start time in query cannot be greater than end time");
        }

        return await _trainingRepository.GetTrainingsByTimeIntervalAsync(startTime, endTime);
    }
}
