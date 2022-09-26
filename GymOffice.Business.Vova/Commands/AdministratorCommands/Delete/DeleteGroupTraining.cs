namespace GymOffice.Business.Commands.AdministratorCommands.Delete;
public class DeleteGroupTraining : IDeleteGroupTraining
{
    private readonly IGroupTrainingRepository _trainingRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICoachRepository _coachRepository;

    public DeleteGroupTraining(IGroupTrainingRepository trainingRepository,
        ICustomerRepository customerRepository, ICoachRepository coachRepository)
    {
        _trainingRepository = trainingRepository;
        _customerRepository = customerRepository;
        _coachRepository = coachRepository;
    }

    public async Task<GroupTraining> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var training = await _trainingRepository.GetByIdAsync(id);
        if (training == null)
        {
            throw new NotFoundException(nameof(GroupTraining), id);
        }

        var coach = await _coachRepository.GetByIdAsync(training.CoachId);
        if (coach == null)
        {
            throw new NotFoundException(nameof(Coach), training.CoachId);
        }

        if (training.Customers != null)
        {
            foreach (var customer in training.Customers)
            {
                await _customerRepository.DeleteGroupTrainingFromCustomerAsync(training.Id, customer.Id);
            }
        }

        await _coachRepository.DeleteGroupTrainingFromCoachAsync(training.Id, coach.Id);

        return await _trainingRepository.DeleteAsync(id);
    }
}
