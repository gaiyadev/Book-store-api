using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookstoreAPI.Models;

[Table("order_items")]
public class OrderItem : BaseEntity
{
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("product_id", TypeName = "int" )]
    public int ProductId { get; set; } 
    
    [JsonIgnore]
    [ForeignKey("ProductId")]
    public  Product Product { get; set; } 
    
    
    [Column("order_id", TypeName = "int" )]
    [ForeignKey("OrderId")]
    public int OrderId { get; set; } 
    
    [JsonIgnore]
    public  Order Order { get; set; } 
    }