namespace LaundryManagement.Api.DTOs;

public class UpdateServiceDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}