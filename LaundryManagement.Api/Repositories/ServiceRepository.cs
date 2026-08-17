using LaundryManagement.Api.Data;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Api.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly LaundryDbContext _context;

    public ServiceRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<Service> CreateAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        return service;
    }

    public async Task<bool> UpdateAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return false;
        }

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();

        return true;
    }
}