namespace LaundryManagement.Api.DTOs;

public class UpdateOrderDto
{
    public string Status { get; set; } = string.Empty;

    public DateTime? CollectionDate { get; set; }
}