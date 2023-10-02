using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using BookstoreAPI.Enums;

namespace BookstoreAPI.Models;

[Table("order_items")]
public class OrderItem : BaseEntity
{
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("payment_status")] 
    public PaymentStatus? PaymentStatus { get; set; } 
    
    [Column("product_id", TypeName = "int" )]
    public int ProductId { get; set; } 
    
    [ForeignKey("ProductId")]
    public  Product Product { get; set; } 
    
    [Column("user_id", TypeName = "int")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    [Column("order_id", TypeName = "int" )]
    [ForeignKey("OrderId")]
    public int OrderId { get; set; } 
    
    public  Order Order { get; set; } 
    }