using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookstoreAPI.Models;

[Table("users")]
public class User : BaseEntity
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
    
    [Column("reset_token",TypeName = "VARCHAR(255)")]
    public string? ResetToken { get; set; }
    
    [Column("otp",TypeName = "VARCHAR(255)")]
    public string? Otp { get; set; }
    
    [Column("is_active", TypeName = "boolean")]
    [DefaultValue(false)]
    public bool IsActive { get; set; }
    
    [Column("role_id")]
    [ForeignKey("role_id")]
    public int RoleId { get; set; }
    
    [Required]
    [Column("role")]
    public Role Role { get; set; }
}