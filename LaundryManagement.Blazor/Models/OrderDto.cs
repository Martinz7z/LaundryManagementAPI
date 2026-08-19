namespace LaundryManagement.Blazor.Models;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }

    public DateTime? CollectionDate { get; set; }

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;
}