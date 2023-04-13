using Application.Common.Authentication;
using Application.Common.Exceptions;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Commands.DeactivateUser;

public sealed class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserPrincipalService _userPrincipalService;

    public DeactivateUserCommandHandler(IUnitOfWork unitOfWork, IUserPrincipalService userPrincipalService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.Users;
        _userPrincipalService = userPrincipalService;
    }

    public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var username = _userPrincipalService.GetUserName() ??
                         throw new UnauthorizedException("User not logged");

        var loggedUser = await _userRepository.GetByUsernameAsync(username) ??
                         throw new NotFoundException($"User with username: {request.username} not found");

        var userToDelete = loggedUser;
        
        if (!loggedUser.Username.Equals(request.username))
        {
            userToDelete = await _userRepository.GetByUsernameAsync(request.username) ??
                               throw new NotFoundException($"User with username: {request.username} not found");
        }

        if (!loggedUser.Username.Equals(request.username) && loggedUser.Role != Role.Admin)
        {
            throw new UnauthorizedException("User not perform to do this");
        }

        _userRepository.Remove(userToDelete);
        await _unitOfWork.SaveAsync();
        
        return Unit.Value;
    }
}