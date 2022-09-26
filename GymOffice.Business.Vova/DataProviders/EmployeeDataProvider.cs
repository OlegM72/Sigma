using GymOffice.Common.Contracts.RepositoryContracts;

namespace GymOffice.Business.DataProviders;
public class EmployeeDataProvider : IEmployeeDataProvider
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeDataProvider(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Administrator>?> GetActiveAdministratorsAsync()
    {
        return await _employeeRepository.GetAdministratorsAsync();
    }

    public async Task<IEnumerable<Employee>?> GetActiveEmployeesAsync()
    {
        return await _employeeRepository.GetActiveEmployeesAsync();
    }

    public async Task<IEnumerable<Receptionist>?> GetActiveReceptionistsAsync()
    {
        return await _employeeRepository.GetActiveReceptionistsAsync();
    }

    public async Task<IEnumerable<Administrator>?> GetAdministratorsAsync()
    {
        var adminList = await _employeeRepository.GetAdministratorsAsync();

        if (adminList == null || adminList.Any() == false)
        {
            throw new ApplicationException("The list of administrators is null or empty");
        }

        return adminList;
    }

    public async Task<IEnumerable<Receptionist>?> GetReceptionistsAsync()
    {
        return await _employeeRepository.GetReceptionistsAsync();
    }

    public async Task<IEnumerable<Employee>?> GetAllEmployeesAsync()
    {
        var employeesList = await _employeeRepository.GetEmployeesAsync();

        if (employeesList == null || employeesList.Any() == false)
        {
            throw new ApplicationException("The list of employees is null or empty");
        }

        return employeesList;
    }

    public async Task<Administrator?> GetAdministratorByIdAsync(Guid id)
    {
        Administrator? entity = await _employeeRepository.GetAdministratorByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(Administrator), id);
        }

        return entity;
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        Employee? entity = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        return entity;
    }

    public async Task<Receptionist?> GetReceptionistByIdAsync(Guid id)
    {
        Receptionist? entity = await _employeeRepository.GetReceptionistByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(Receptionist), id);
        }

        return entity;
    }

    public async Task<IEnumerable<Receptionist>?> SearchReceptionistsAsync(ReceptionistSearchOptions options)
    {
        return await _employeeRepository.SearchReceptionistsAsync(options);
    }
}
