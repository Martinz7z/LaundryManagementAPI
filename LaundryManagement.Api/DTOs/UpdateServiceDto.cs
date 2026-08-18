using System.ComponentModel.DataAnnotations;

namespace LaundryManagement.Api.DTOs;

public class UpdateServiceDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 10000)]
    public decimal Price { get; set; }
}