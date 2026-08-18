using LaundryManagement.Api.Data;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LaundryDbContext _context;

    public OrderRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(order => order.Customer)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(order => order.Customer)
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task RecalculateTotalAsync(int orderId)
{
    var order = await _context.Orders.FindAsync(orderId);

    if (order == null)
    {
        return;
    }

    order.TotalPrice = await _context.OrderItems
        .Where(item => item.OrderId == orderId)
        .SumAsync(item => item.Price * item.Quantity);

    await _context.SaveChangesAsync();
}
}