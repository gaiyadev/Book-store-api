using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.DTOs;

public class CartItemDto
{
    [Required]
    public required int ProductId { get; set; }

    [Required] 
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public required int Quantity { get; set; }
}

