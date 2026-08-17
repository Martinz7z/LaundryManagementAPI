using LaundryManagement.Api.Models;

namespace LaundryManagement.Api.Interfaces;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();

    Task<OrderItem?> GetByIdAsync(int id);

    Task<OrderItem> CreateAsync(OrderItem orderItem);

    Task<bool> UpdateAsync(OrderItem orderItem);

    Task<bool> DeleteAsync(int id);
}