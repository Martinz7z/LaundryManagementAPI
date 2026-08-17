using LaundryManagement.Api.Models;

namespace LaundryManagement.Api.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int id);

    Task<Customer> CreateAsync(Customer customer);

    Task<bool> UpdateAsync(Customer customer);

    Task<bool> DeleteAsync(int id);
}   