using System.Security.Claims;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity.Policies.UserNotBannedRequirement;

public class UserNotBannedRequirementHandler : AuthorizationHandler<UserNotBannedRequirment.UserNotBannedRequirement>
{
    private readonly IUserRepository _userRepository;

    public UserNotBannedRequirementHandler(IUnitOfWork unitOfWork)
    {
        _userRepository = unitOfWork.Users;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserNotBannedRequirment.UserNotBannedRequirement requirement)
    {
        var username = context
            .User
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        if (username is not null)
        {
            var isUserBanned = await _userRepository.IsUserBanned(username);

            if (!isUserBanned)
            {
                context.Succeed(requirement);
            }
        }
    }
}