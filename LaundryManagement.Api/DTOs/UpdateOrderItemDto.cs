using System.ComponentModel.DataAnnotations;

namespace LaundryManagement.Api.DTOs;

public class UpdateOrderItemDto
{
    [Range(1, 100)]
    public int Quantity { get; set; }
}