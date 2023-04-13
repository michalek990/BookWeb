using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity.Policies.UserNotBannedRequirement;

public class UserNotBannedRequirement : IAuthorizationRequirement { }