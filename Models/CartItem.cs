using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookstoreAPI.Models;

[Table("cart_items")]
public class CartItem : BaseEntity
{
    [Column("quantity", TypeName = "int")]
    [Required]
    public int Quantity { get; set; }
    
    [Column("price")]
    [Required]
    public int Price { get; set; }

    
    [Column("user_id", TypeName = "int" )]
    public int UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
    
    [Column("product_id", TypeName = "int" )]
    public int ProductId { get; set; } 

    [JsonIgnore]
    public Product Product { get; set; } 


}