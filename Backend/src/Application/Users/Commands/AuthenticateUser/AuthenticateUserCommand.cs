using Application.Models;
using MediatR;

namespace Application.Users.Commands.AuthenticateUser;

public sealed record AuthenticateUserCommand(string username, string password) : IRequest<JwtTokenDto>;
