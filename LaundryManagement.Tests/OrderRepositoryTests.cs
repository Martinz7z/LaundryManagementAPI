using LaundryManagement.Api.Data;
using LaundryManagement.Api.Models;
using LaundryManagement.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LaundryManagement.Tests;

public class OrderRepositoryTests
{
    private LaundryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LaundryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new LaundryDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsOrder()
    {
        using var context = CreateContext();

        var customer = new Customer
        {
            Name = "Test Customer",
            Email = "customer@example.com",
            Phone = "0871234567"
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        var order = new Order
        {
            CustomerId = customer.Id,
            Status = "Pending",
            TotalPrice = 0
        };

        var result = await repository.CreateAsync(order);

        Assert.NotEqual(0, result.Id);
        Assert.Equal("Pending", result.Status);
        Assert.Single(context.Orders);
    }

    [Fact]
    public async Task RecalculateTotalAsync_CalculatesCorrectTotal()
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

        var order = new Order
        {
            CustomerId = customer.Id,
            Status = "Pending",
            TotalPrice = 0
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var service1 = new Service
        {
            Name = "Wash and Dry",
            Description = "Standard service",
            Price = 12.50m
        };

        var service2 = new Service
        {
            Name = "Duvet Cleaning",
            Description = "Duvet service",
            Price = 20.00m
        };

        context.Services.AddRange(service1, service2);
        await context.SaveChangesAsync();

        context.OrderItems.AddRange(
            new OrderItem
            {
                OrderId = order.Id,
                ServiceId = service1.Id,
                Quantity = 2,
                Price = service1.Price
            },
            new OrderItem
            {
                OrderId = order.Id,
                ServiceId = service2.Id,
                Quantity = 1,
                Price = service2.Price
            }
        );

        await context.SaveChangesAsync();

        var repository = new OrderRepository(context);

        await repository.RecalculateTotalAsync(order.Id);

        var updatedOrder = await context.Orders.FindAsync(order.Id);

        Assert.NotNull(updatedOrder);
        Assert.Equal(45.00m, updatedOrder.TotalPrice);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenOrderDoesNotExist()
    {
        using var context = CreateContext();
        var repository = new OrderRepository(context);

        var result = await repository.DeleteAsync(999);

        Assert.False(result);
    }
}