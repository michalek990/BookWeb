using System.Text;
using Application.Common.Authentication;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Identity;
using Infrastructure.Identity.Policies.UserNotBannedRequirement;
using Infrastructure.Presistence;
using Infrastructure.Presistence.Pipeline;
using Infrastructure.Presistence.Pipeline.Operations;
using Infrastructure.Presistence.Pipeline.Operations.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>();
    }
    
    public static void AddBeforeSaveChangesPipeline(this IServiceCollection services)
    {
        services.AddScoped<IAddCreationInfoOperation, AddCreationInfoOperation>();
        services.AddScoped<IAddUpdateInfoOperation, AddUpdateInfoOperation>();
        services.AddScoped<IRemovalHandlingOperation, RemovalHandlingOperation>();
        services.AddScoped<IBeforeSaveChangesPipelineBuilder, BeforeSaveChangesPipelineBuilder>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserPasswordHasher, UserPasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserPrincipalService, UserPrincipalService>();

        var authenticationSettings = new AuthenticationSettings();
        services.AddSingleton(authenticationSettings);
        
        configuration.GetSection("Authentication").Bind(authenticationSettings);
        ValidateAuthenticationSettings(authenticationSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = !environment.IsDevelopment();
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authenticationSettings.Issuer,
                ValidAudience = authenticationSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret!))
            };
        });
    }

    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AccountActive", builder =>
            {
                builder.AddRequirements(new UserNotBannedRequirement());
            });
        });

        services.AddScoped<IAuthorizationHandler, UserNotBannedRequirementHandler>();
    }

    private static void ValidateAuthenticationSettings(AuthenticationSettings settings)
    {
        if (settings.Secret is null
            || settings.ExpireDays is null
            || settings.Issuer is null
            || settings.Audience is null)
        {
            //throw new AppconfigurationException("One or more of the required authentication settings is missing");
        }
    }
}