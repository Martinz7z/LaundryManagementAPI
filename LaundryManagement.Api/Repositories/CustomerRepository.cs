using LaundryManagement.Api.Data;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly LaundryDbContext _context;

    public CustomerRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return false;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }
}