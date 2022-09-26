namespace GymOffice.Business.Commands.AdministratorCommands.Delete;
public class DeleteEmployeeCommand : IDeleteEmployeeCommand
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommand(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> ExecuteAsync(Guid employeeId)
    {
        Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), employeeId);
        }

        return await _employeeRepository.DeleteEmployeeAsync(employeeId);
    }
}
