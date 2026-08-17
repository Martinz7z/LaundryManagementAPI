namespace LaundryManagement.Api.DTOs;

public class CreateOrderItemDto
{
    public int OrderId { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; }
}