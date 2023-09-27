using FluentValidation;

namespace BookstoreAPI.DTOs;

public class SignupDto
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; }
    public int RoleId { get; set; }
}

public class SignupDtoValidator : AbstractValidator<SignupDto>
{
    public SignupDtoValidator()
    {
        RuleFor(rule => rule.Email).NotEmpty().WithMessage("Email should not be empty").EmailAddress();
        
        RuleFor(rule => rule.Username).NotEmpty().WithMessage("Username should not be empty")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Username must only contain letters, and spaces.");
        
        RuleFor(rule => rule.RoleId).NotEmpty().WithMessage("Role should not be empty");
        
        RuleFor(rule => rule.Password).NotEmpty().WithMessage("Password should not be empty")
            .MinimumLength(6).WithMessage("Password too short");
        
        RuleFor(rule => rule.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword should not be empty")
            .Equal(p => p.Password).WithMessage("Password mismatch");
    }
}