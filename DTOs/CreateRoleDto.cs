using FluentValidation;

namespace BookstoreAPI.DTOs;

public class CreateRoleDto
{
    public string Name { get; set; } = string.Empty;
}


public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(role => role.Name).NotEmpty().WithMessage("role should not be empty")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Title must only contain letters, and spaces.");
    }
}