using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity.Policies.UserNotBannedRequirment;

public class UserNotBannedRequirement : IAuthorizationRequirement { }