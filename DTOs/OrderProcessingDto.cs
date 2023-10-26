using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Pending,
    Processing
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ValidOrderStatusAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string status)
        {
            if (Enum.TryParse(typeof(OrderStatus), status, true, out _))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Invalid Order Status. Allowed values are 'Pending' or 'Processing'.");
    }
}

public class OrderProcessingDto
{
    [Required]
    [ValidOrderStatus]
    public string Status { get; set; }
}