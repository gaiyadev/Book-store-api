using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.DTOs;

public class VerifyResetPasswordOtp
{
    [Required]
    public string OTP { get; set; }
}