using System.ComponentModel.DataAnnotations;

namespace LaundryManagement.Api.DTOs;

public class UpdateCustomerDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}