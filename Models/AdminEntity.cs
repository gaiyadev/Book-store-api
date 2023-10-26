using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookstoreAPI.Models;

[Table("admins")]
public class Admin : BaseEntity
{
    [Required]
    [Column("email", TypeName = "VARCHAR(255)")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("username",TypeName = "VARCHAR(255)")] 
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [JsonIgnore]
    [Column("password", TypeName = "VARCHAR(255)")]
    public string Password { get; set; } = string.Empty;
    
    
    [Column("is_active", TypeName = "boolean")]
    [DefaultValue(true)]
    public bool IsActive { get; set; }
    
    [Column("role_id")]
    [ForeignKey("role_id")]
    public int RoleId { get; set; }
    
    [Required]
    [Column("role")]
    public Role Role { get; set; }
}