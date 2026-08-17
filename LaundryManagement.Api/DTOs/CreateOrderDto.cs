namespace LaundryManagement.Api.DTOs;

public class CreateOrderDto
{
    public int CustomerId { get; set; }

    public DateTime? CollectionDate { get; set; }
}