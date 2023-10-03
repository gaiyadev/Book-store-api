using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookstoreAPI.Models;

[Table("orders")]
public class Order : BaseEntity
{
    [Column("order_date")]
    public DateTime OrderDate { get; set; }
    
    [Column("total_amount")]
    public decimal TotalAmount { get; set; }
    
    [Column("shipping_address", TypeName = "VARCHAR(255)")]
    public string ShippingAddress { get; set; }
    
    [Column("billing_address", TypeName = "VARCHAR(255)")]
    public string BillingAddress { get; set; }
    
    [Column("delivery_fees")]
    public string DeliveryFees { get; set; }
    
    [Column("order_status")]
    // [DefaultValue(OrderStatus.Pending)]
    public string Status { get; set; } 
    
    [Column("payment_method")] 
    public string PaymentMethod { get; set; } 
    
    [Column("delivery_method")] 
    public string DeliveryMethod { get; set; }

    [Column("phone_number")] 
    public string PhoneNumber { get; set; }
    
    [Column("payment_status")] 
    public string? PaymentStatus { get; set; }

    [Column("tracking_id")] 
    public string TrackingId { get; set; }

    [JsonIgnore]
    public ICollection<OrderItem> OrderItems { get; set; }
    
    [Column("user_id", TypeName = "int" )]
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User User { get; set; }
}