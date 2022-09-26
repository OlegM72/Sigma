namespace GymOffice.Business.Commands.EmployeeCommands.Add;
public class AddCustomerToGroupTrainingCommand : IAddCustomerToGroupTrainingCommand
{
    private readonly IGroupTrainingRepository _trainingRepository;
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerToGroupTrainingCommand(IGroupTrainingRepository trainingRepository, ICustomerRepository customerRepository)
    {
        _trainingRepository = trainingRepository;
        _customerRepository = customerRepository;
    }

    public async Task ExecuteAsync(Guid trainingId, Guid customerId)
    {
        GroupTraining? training = await _trainingRepository.GetByIdAsync(trainingId);
        if (training == null)
        {
            throw new NotFoundException(nameof(GroupTraining), trainingId);
        }
        if (training.Customers?.Count >= training.MaxCustomersNumber)
        {
            throw new CannotAddCustomerToTrainingException(training.Name);
        }

        Customer? customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new NotFoundException(nameof(Customer), customerId);
        }

        await _trainingRepository.AddCustomerToTrainingAsync(training, customer);
    }
}
