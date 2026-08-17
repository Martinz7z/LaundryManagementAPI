using LaundryManagement.Api.Models;

namespace LaundryManagement.Api.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();

    Task<Service?> GetByIdAsync(int id);

    Task<Service> CreateAsync(Service service);

    Task<bool> UpdateAsync(Service service);

    Task<bool> DeleteAsync(int id);
}