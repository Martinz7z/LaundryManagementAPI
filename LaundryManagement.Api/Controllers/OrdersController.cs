using LaundryManagement.Api.DTOs;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrdersController(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var orders = await _orderRepository.GetAllAsync();

        var orderDtos = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            CollectionDate = order.CollectionDate,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer.Name
        });

        return Ok(orderDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        var orderDto = new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            CollectionDate = order.CollectionDate,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer.Name
        };

        return Ok(orderDto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(
        CreateOrderDto createOrderDto)
    {
        var customer = await _customerRepository.GetByIdAsync(
            createOrderDto.CustomerId);

        if (customer == null)
        {
            return BadRequest("Customer does not exist.");
        }

        var order = new Order
        {
            CustomerId = createOrderDto.CustomerId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            TotalPrice = 0,
            CollectionDate = createOrderDto.CollectionDate
        };

        await _orderRepository.CreateAsync(order);

        var orderDto = new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            CollectionDate = order.CollectionDate,
            CustomerId = order.CustomerId,
            CustomerName = customer.Name
        };

        return CreatedAtAction(
            nameof(GetOrder),
            new { id = order.Id },
            orderDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(
        int id,
        UpdateOrderDto updateOrderDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        order.Status = updateOrderDto.Status;
        order.CollectionDate = updateOrderDto.CollectionDate;

        await _orderRepository.UpdateAsync(order);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _orderRepository.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}