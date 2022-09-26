namespace GymOffice.Business.Commands.AdministratorCommands.Add;
public class AddAdministratorCommand : IAddAdministratorCommand
{
    private readonly IEmployeeRepository _employeeRepository;

    public AddAdministratorCommand(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Guid> ExecuteAsync(Administrator administrator)
    {
        if (administrator == null)
        {
            throw new ArgumentNullException(nameof(administrator));
        }
        if (administrator.FirstName == null ||
            administrator.LastName == null ||
            administrator.Email == null ||
            administrator.Phone == null)
        {
            throw new RequiredPropertiesNotFilledException();
        }
        if (await _employeeRepository.GetEmployeeByIdAsync(administrator.Id) != null)
        {
            throw new SameEntityExistsException(nameof(Administrator), administrator.Id);
        }

        return await _employeeRepository.AddEmployeeAsync(administrator);
    }
}
