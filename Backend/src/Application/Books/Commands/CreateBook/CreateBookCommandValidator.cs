using FluentValidation;

namespace Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotNull()
            .MaximumLength(100);
        
        RuleFor(b => b.Author)
            .NotNull()
            .MaximumLength(75);
        
        RuleFor(b => b.Description)
            .MaximumLength(1000);
        
        RuleFor(b => b.Cover)
            .NotNull()
            .MaximumLength(1000);
    }
}