using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.DTOs;

public class UpdateCartItemDto
{
    [Required]
    public required int Quantity { get; set; }
}