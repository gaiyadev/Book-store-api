using FluentValidation;

namespace BookstoreAPI.DTOs;

public class CreateProductDto
{
    public string Title { get; set; } = string.Empty;

    public int Price { get; set; } = 0;

    public string Author { get; set; } = string.Empty;
    
    public int GenreId { get; set; }

    public IFormFile File { get; set; }
    

}

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(rule=> rule.Title)
            .NotEmpty()
            .WithMessage("Title should not be empty.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Title must only contain letters, and spaces.");
        
        RuleFor(rule=> rule.Author)
            .NotEmpty()
            .WithMessage("Author should not be empty.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Author must only contain letters, and spaces.");
        
        RuleFor(rule => rule.Price)
            .NotEmpty()
            .WithMessage("Price should not be empty.")
            .WithMessage("Price must be a valid number with up to two decimal places.");
        
        RuleFor(rule => rule.GenreId)
            .NotEmpty()
            .WithMessage("Genre should not be empty.")
            .WithMessage("Genre must be a valid number with up to two decimal places.");

        RuleFor(rule => rule.File)
            .NotEmpty()
            .WithMessage("Product image should not be empty.");
    }
    }
