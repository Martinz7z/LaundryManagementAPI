using System.ComponentModel.DataAnnotations;

namespace LaundryManagement.Api.DTOs;

public class CreateOrderItemDto
{
    [Range(1, int.MaxValue)]
    public int OrderId { get; set; }

    [Range(1, int.MaxValue)]
    public int ServiceId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }
}