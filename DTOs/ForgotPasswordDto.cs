using FluentValidation;

namespace BookstoreAPI.DTOs;

public class ForgotPasswordDto
{
    public string Email { get; set; }
}

class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordDtoValidator()
    {
        RuleFor(rule => rule.Email)
            .NotEmpty()
            .WithMessage("Email should not be empty")
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}