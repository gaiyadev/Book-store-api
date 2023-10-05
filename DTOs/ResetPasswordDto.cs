using System.Reflection.PortableExecutable;
using FluentValidation;

namespace BookstoreAPI.DTOs;

public class ResetPasswordDto
{
    public required string NewPassword { get; set; }
    
    public required string ConfirmPassword { get; set; }
}

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        
        RuleFor(rule => rule.NewPassword)
            .NotEmpty().WithMessage("ConfirmPassword should not be empty")
            .MinimumLength(6).WithMessage("Password too short");
        
        RuleFor(rule => rule.ConfirmPassword)
            .NotEmpty()
            .WithMessage("ConfirmPassword should not be empty")
            .Equal(p => p.NewPassword).WithMessage("Password mismatch");
    }
}