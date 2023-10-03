using FluentValidation;

namespace BookstoreAPI.DTOs;

public class OrderCreateDto
{
  
    public string ShippingAddress { get; set; }= string.Empty;

    public string BillingAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; }

    public double Latitude { get; set; }
    
    public double Longitude { get; set; }

    public string PaymentMethod { get; set; }

    public string DeliveryMethod { get; set; }
    
    public string PaymentStatus { get; set; }
    public List<CartItemDto> CartItems { get; set; }
}

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto> {
    public OrderCreateDtoValidator()
    {
        RuleFor(rule => rule.ShippingAddress).NotEmpty()
            .WithMessage("ShippingAddress should not be empty");
        
        RuleFor(rule => rule.BillingAddress).NotEmpty()
            .WithMessage("BillingAddress should not be empty");
        
        RuleFor(rule => rule.PhoneNumber).NotEmpty()
            .WithMessage("PhoneNumber should not be empty");
        
        RuleFor(rule => rule.CartItems).NotEmpty()
            .WithMessage("CartItems should not be empty");
        
        RuleFor(rule => rule.Latitude).NotEmpty()
            .WithMessage("Latitude should not be empty");
        
        RuleFor(rule => rule.Longitude).NotEmpty()
            .WithMessage("Latitude should not be empty");

        RuleFor(rule => rule.PaymentStatus)
            .NotEmpty()
            .WithMessage("PaymentStatus should not be empty")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("PaymentStatus must only contain letters, and spaces.");
        
        RuleFor(rule => rule.PaymentMethod)
            .NotEmpty()
            .WithMessage("PaymentMethod should not be empty")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("PaymentMethod must only contain letters, and spaces.");
        
        RuleFor(rule => rule.DeliveryMethod)
            .NotEmpty()
            .WithMessage("DeliveryMethod should not be empty")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("DeliveryMethod must only contain letters, and spaces.");
    }
}