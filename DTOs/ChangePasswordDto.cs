﻿using FluentValidation;

namespace BookstoreAPI.DTOs;

public class ChangePasswordDto
{
    public required string Password { get; set; }
    
    public required string NewPassword { get; set; }
    
    public required string ConfirmPassword { get; set; }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        
        RuleFor(rule => rule.Password)
            .NotEmpty().WithMessage("Password should not be empty")
            .MinimumLength(6).WithMessage("Password too short");
        
        RuleFor(rule => rule.NewPassword)
            .NotEmpty().WithMessage("ConfirmPassword should not be empty")
            .MinimumLength(6).WithMessage("Password too short");
        
        RuleFor(rule => rule.ConfirmPassword)
            .NotEmpty()
            .WithMessage("ConfirmPassword should not be empty")
            .Equal(p => p.NewPassword).WithMessage("Password mismatch");
    }
}
