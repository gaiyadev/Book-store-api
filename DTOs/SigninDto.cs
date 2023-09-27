using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.DTOs;

public class SigninDto
{
    [Required] 
    public required string LoginId { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Password too short")]
    public required string Password { get; set; } = string.Empty;
}