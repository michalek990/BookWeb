using MediatR;

namespace Application.Users.Commands.DeactivateUser;

public sealed record DeactivateUserCommand(string? username) : IRequest<Unit>;