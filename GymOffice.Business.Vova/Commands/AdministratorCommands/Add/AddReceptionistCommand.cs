namespace GymOffice.Business.Commands.AdministratorCommands.Add;
public class AddReceptionistCommand : IAddReceptionistCommand
{
    private readonly IEmployeeRepository _employeeRepository;

    public AddReceptionistCommand(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Guid> ExecuteAsync(Receptionist receptionist)
    {
        if (receptionist == null)
        {
            throw new ArgumentNullException(nameof(receptionist));
        }
        if (receptionist.FirstName == null ||
            receptionist.LastName == null ||
            receptionist.Email == null ||
            receptionist.Phone == null)
        {
            throw new RequiredPropertiesNotFilledException();
        }
        if (await _employeeRepository.GetEmployeeByIdAsync(receptionist.Id) != null)
        {
            throw new SameEntityExistsException(nameof(Receptionist), receptionist.Id);
        }

        return await _employeeRepository.AddEmployeeAsync(receptionist);
    }
}
