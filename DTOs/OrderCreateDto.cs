namespace BookstoreAPI.DTOs;

public class OrderCreateDto
{
  
    public string ShippingAddress { get; set; }

    public string BillingAddress { get; set; }
    
    public List<CartItemDto> CartItems { get; set; }

}