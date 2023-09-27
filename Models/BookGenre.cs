using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookstoreAPI.Models;

[Table("book_genres")]
public class BookGenre : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; } = string.Empty; 
    
    [JsonIgnore]
    public List<Product> Products { get; set; } = new List<Product>();

}