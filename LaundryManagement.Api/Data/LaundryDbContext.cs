using LaundryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Api.Data;

public class LaundryDbContext : DbContext
{
    public LaundryDbContext(DbContextOptions<LaundryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Service> Services => Set<Service>();
}