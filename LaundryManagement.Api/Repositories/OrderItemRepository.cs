using LaundryManagement.Api.Data;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Api.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly LaundryDbContext _context;

    public OrderItemRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _context.OrderItems
            .Include(item => item.Service)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _context.OrderItems
            .Include(item => item.Service)
            .FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        return orderItem;
    }

    public async Task<bool> UpdateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);

        if (orderItem == null)
        {
            return false;
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();

        return true;
    }
}