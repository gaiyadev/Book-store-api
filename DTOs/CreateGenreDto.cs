using FluentValidation;

namespace BookstoreAPI.DTOs;

public class CreateGenreDto
{
    public string Name { get; set; } = string.Empty;
}

public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreDtoValidator()
    {

        RuleFor(genre => genre.Name)
            .NotEmpty()
            .WithMessage("Name should not be empty.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Title must only contain letters, and spaces.");
    }
}