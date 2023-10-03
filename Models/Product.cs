using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BookstoreAPI.Models;

[Table("products")]
public class Product : BaseEntity
{
    [Required]
    [Column("title",TypeName = "VARCHAR(255)")] 
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [Column("author", TypeName = "VARCHAR(255)")]
    public string Author { get; set; } = string.Empty;

    [Required]
    [Column("product_slug", TypeName = "VARCHAR(255)")]
    public string ProductSlug { get; set; } = string.Empty;
    
    [Required]
    [Column("product_url", TypeName = "VARCHAR(255)")]
    public string ProductUrl { get; set; } = string.Empty;
            
    [Required]
    [Column("price")]
    public int Price { get; set; }
    
    [Required]
    [Column("Is_available",  TypeName = "boolean")]
    [DefaultValue(true)]
    public bool IsAvailable { get; set; }
    
    [Column("user_id", TypeName = "int" )]
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User User { get; set; }
    
    [Column("Book_Genre_id", TypeName = "int" )]
    public int BookGenreId { get; set; }
    
    public BookGenre BookGenre { get; set; }
    
    [JsonIgnore]
    public List<CartItem> CartItems { get; set; } 


}