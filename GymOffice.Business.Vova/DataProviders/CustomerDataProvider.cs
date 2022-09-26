namespace GymOffice.Business.DataProviders;
public class CustomerDataProvider : ICustomerDataProvider
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerDataProvider(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>?> GetActiveCustomersAsync()
    {
        return await _customerRepository.GetActiveCustomersAsync();
    }

    public async Task<IEnumerable<Customer>?> GetAllCustomersAsync()
    {
        return await _customerRepository.GetCustomersAsync();
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _customerRepository.GetByEmailAsync(email);
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        Customer? entity = await _customerRepository.GetByIdAsync(id);

        if (entity == null || entity.Id != id)
        {
            throw new NotFoundException(nameof(Customer), id);
        }

        return entity;
    }

    public async Task<Customer?> GetCustomerByPhoneAsync(string phoneNumber)
    {        
        return await _customerRepository.GetByPhoneAsync(phoneNumber);
    }

    public async Task<IEnumerable<Customer>?> GetCustomersByFirstNameAsync(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return await _customerRepository.GetCustomersAsync();
        }

        return await _customerRepository.GetCustomersByFirstNameAsync(firstName);
    }

    public async Task<IEnumerable<Customer>?> GetCustomersByLastNameAsync(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            return await _customerRepository.GetCustomersAsync();
        }

        return await _customerRepository.GetCustomersByLastNameAsync(lastName);
    }

    public async Task<IEnumerable<Customer>?> GetCustomersByRegistrationDateAsync(DateTime? startDate, DateTime? endDate)
    {
        startDate ??= new DateTime(2022, 1, 1);
        endDate ??= DateTime.Now;

        if (startDate > endDate)
        {
            throw new ArgumentException("The start date cannot be greater than the end date");
        }
        if (startDate > DateTime.Now)
        {
            throw new ArgumentException("The start date cannot be in future");
        }

        return await _customerRepository.GetCustomersByRegistrationDateAsync(startDate, endDate);
    }

    public async Task<IEnumerable<Customer>?> SerchCustomersAsync(CustomerSearchOptions options)
    {
        options.StartDate ??= new DateTime(2022, 1, 1);
        options.EndDate ??= DateTime.Now;

        if (options.StartDate > options.EndDate)
        {
            throw new ArgumentException("The start date cannot be greater than the end date");
        }
        if (options.StartDate > DateTime.Now)
        {
            throw new ArgumentException("The start date cannot be in future");
        }

        return await _customerRepository.SearchCustomersAsync(options);
    }
}
