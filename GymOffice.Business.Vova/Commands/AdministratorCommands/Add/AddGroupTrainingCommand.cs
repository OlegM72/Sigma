namespace GymOffice.Business.Commands.AdministratorCommands.Add;
public class AddGroupTrainingCommand : IAddGroupTrainingCommand
{
    private readonly IGroupTrainingRepository _trainingRepository;
    private readonly ICoachRepository _coachRepository;

    public AddGroupTrainingCommand(IGroupTrainingRepository trainingRepository, ICoachRepository coachRepository)
    {
        _trainingRepository = trainingRepository;
        _coachRepository = coachRepository;
    }

    public async Task<Guid> ExecuteAsync(GroupTraining groupTraining)
    {
        if (groupTraining == null)
        {
            throw new ArgumentNullException(nameof(groupTraining));
        }
        if (groupTraining.Name == null || groupTraining.CoachId == Guid.Empty)
        {
            throw new RequiredPropertiesNotFilledException();
        }
        if (groupTraining.StartTime >= groupTraining.EndTime)
        {
            throw new ArgumentException("End time of training must be greater than start time");
        }
        if (await _trainingRepository.GetByIdAsync(groupTraining.Id) != null)
        {
            throw new SameEntityExistsException(nameof(GroupTraining), groupTraining.Id);
        }
        if (await _coachRepository.GetByIdAsync(groupTraining.CoachId) == null)
        {
            throw new NotFoundException(nameof(Coach), groupTraining.CoachId);
        }
        if (groupTraining.MaxCustomersNumber == default)
        {
            groupTraining.MaxCustomersNumber = 10;
        }

        await _coachRepository.AddGroutTrainingToCouchAsync(groupTraining, groupTraining.CoachId);

        return await _trainingRepository.AddTrainingAsync(groupTraining);
    }
}
