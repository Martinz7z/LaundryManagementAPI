using LaundryManagement.Api.Data;
using LaundryManagement.Api.Models;
using LaundryManagement.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Tests;

public class CustomerRepositoryTests
{
    private LaundryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LaundryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new LaundryDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsCustomer()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customer = new Customer
        {
            Name = "Test Customer",
            Email = "test@example.com",
            Phone = "0871234567"
        };

        var result = await repository.CreateAsync(customer);

        Assert.NotEqual(0, result.Id);
        Assert.Equal("Test Customer", result.Name);
        Assert.Single(context.Customers);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCustomer_WhenCustomerExists()
    {
        using var context = CreateContext();

        var customer = new Customer
        {
            Name = "Sarah Kelly",
            Email = "sarah@example.com",
            Phone = "0875551234"
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        var repository = new CustomerRepository(context);

        var result = await repository.GetByIdAsync(customer.Id);

        Assert.NotNull(result);
        Assert.Equal("Sarah Kelly", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenCustomerDoesNotExist()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesCustomer()
    {
        using var context = CreateContext();

        var customer = new Customer
        {
            Name = "Old Name",
            Email = "old@example.com",
            Phone = "0871111111"
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        var repository = new CustomerRepository(context);

        customer.Name = "New Name";
        customer.Email = "new@example.com";

        await repository.UpdateAsync(customer);

        var updatedCustomer = await context.Customers.FindAsync(customer.Id);

        Assert.NotNull(updatedCustomer);
        Assert.Equal("New Name", updatedCustomer.Name);
        Assert.Equal("new@example.com", updatedCustomer.Email);
    }

    [Fact]
    public async Task DeleteAsync_RemovesCustomer()
    {
        using var context = CreateContext();

        var customer = new Customer
        {
            Name = "Delete Me",
            Email = "delete@example.com",
            Phone = "0872222222"
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        var repository = new CustomerRepository(context);

        var result = await repository.DeleteAsync(customer.Id);

        Assert.True(result);
        Assert.Empty(context.Customers);
    }
}