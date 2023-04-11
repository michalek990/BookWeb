using FluentValidation;

namespace Application.Users.Commands.AuthenticateUser;

public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(u => u.username)
            .NotNull();
        
        RuleFor(u => u.password)
            .NotNull();
    }
}