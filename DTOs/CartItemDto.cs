using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.DTOs;

public class CartItemDto
{
    [Required]
    public required int ProductId { get; set; }
    
    [Required]
    public required int Quantity { get; set; }
}

