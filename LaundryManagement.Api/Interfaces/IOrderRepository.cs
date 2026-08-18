using LaundryManagement.Api.Models;

namespace LaundryManagement.Api.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(int id);

    Task<Order> CreateAsync(Order order);

    Task<bool> UpdateAsync(Order order);

    Task<bool> DeleteAsync(int id);

    Task RecalculateTotalAsync(int orderId);
}