using LaundryManagement.Api.DTOs;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IServiceRepository _serviceRepository;

    public OrderItemsController(
        IOrderItemRepository orderItemRepository,
        IOrderRepository orderRepository,
        IServiceRepository serviceRepository)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems()
    {
        var items = await _orderItemRepository.GetAllAsync();

        var itemDtos = items.Select(item => new OrderItemDto
        {
            Id = item.Id,
            OrderId = item.OrderId,
            ServiceId = item.ServiceId,
            ServiceName = item.Service.Name,
            Quantity = item.Quantity,
            Price = item.Price,
            Subtotal = item.Price * item.Quantity
        });

        return Ok(itemDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
    {
        var item = await _orderItemRepository.GetByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        var itemDto = new OrderItemDto
        {
            Id = item.Id,
            OrderId = item.OrderId,
            ServiceId = item.ServiceId,
            ServiceName = item.Service.Name,
            Quantity = item.Quantity,
            Price = item.Price,
            Subtotal = item.Price * item.Quantity
        };

        return Ok(itemDto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(
        CreateOrderItemDto createDto)
    {
        var order = await _orderRepository.GetByIdAsync(createDto.OrderId);

        if (order == null)
        {
            return BadRequest("Order does not exist.");
        }

        var service = await _serviceRepository.GetByIdAsync(createDto.ServiceId);

        if (service == null)
        {
            return BadRequest("Service does not exist.");
        }

        if (createDto.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        var orderItem = new OrderItem
        {
            OrderId = createDto.OrderId,
            ServiceId = createDto.ServiceId,
            Quantity = createDto.Quantity,
            Price = service.Price
        };

        await _orderItemRepository.CreateAsync(orderItem);

        var itemDto = new OrderItemDto
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ServiceId = orderItem.ServiceId,
            ServiceName = service.Name,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price,
            Subtotal = orderItem.Price * orderItem.Quantity
        };

        return CreatedAtAction(
            nameof(GetOrderItem),
            new { id = orderItem.Id },
            itemDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(
        int id,
        UpdateOrderItemDto updateDto)
    {
        if (updateDto.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        var orderItem = await _orderItemRepository.GetByIdAsync(id);

        if (orderItem == null)
        {
            return NotFound();
        }

        orderItem.Quantity = updateDto.Quantity;

        await _orderItemRepository.UpdateAsync(orderItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var deleted = await _orderItemRepository.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}