namespace GymOffice.Business.Commands.EmployeeCommands.Add;
public class AddCustomerCommand : IAddCustomerCommand
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerCommand(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> ExecuteAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }
        if (customer.FirstName == null ||
            customer.LastName == null ||
            customer.Phone == null)
        {
            throw new RequiredPropertiesNotFilledException();
        }
        if (await _customerRepository.GetByIdAsync(customer.Id) != null)
        {
            throw new SameEntityExistsException(nameof(Administrator), customer.Id);
        }

        return await _customerRepository.AddCustomerAsync(customer);
    }
}
