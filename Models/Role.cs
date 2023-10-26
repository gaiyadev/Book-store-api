using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookstoreAPI.Models;

[Table("roles")]
public class 
    Role : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<User> Users { get; set; } // One role can have many users
    
    [JsonIgnore]
    public List<Admin> Admins { get; set; }
}