using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using BookstoreAPI.Enums;
using Newtonsoft.Json;

namespace BookstoreAPI.Models;

[Table("orders")]
public class Order : BaseEntity
{
    [Column("order_date", TypeName = "VARCHAR(255)")]
    public DateTime OrderDate { get; set; }
    
    [Column("total_amount", TypeName = "VARCHAR(255)")]
    public decimal TotalAmount { get; set; }
    
    [Column("shipping_address", TypeName = "VARCHAR(255)")]
    public string ShippingAddress { get; set; }
    
    [Column("billing_address", TypeName = "VARCHAR(255)")]
    public string BillingAddress { get; set; }
    
    [Column("status")]
    [DefaultValue(OrderStatus.Pending)]
    public OrderStatus Status { get; set; } 

    [JsonIgnore]
    public List<OrderItem> OrderItems { get; set; }
    
    [Column("user_id", TypeName = "int" )]
    public int UserId { get; set; }
    
    public User User { get; set; }
}