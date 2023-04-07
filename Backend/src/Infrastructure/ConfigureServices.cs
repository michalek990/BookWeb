using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Pipeline;
using Infrastructure.Persistence.Pipeline.Operations;
using Infrastructure.Persistence.Pipeline.Operations.Interfaces;
using Infrastructure.Presistence;
using Infrastructure.Repositories;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    public static void AddBeforeSaveChangesPipeline(this IServiceCollection services)
    {
        services.AddScoped<IAddCreationInfoOperation, AddCreationInfoOperation>();
        services.AddScoped<IAddUpdateInfoOperation, AddUpdateInfoOperation>();
        services.AddScoped<IRemovalHandlingOperation, RemovalHandlingOperation>();
        services.AddScoped<IBeforeSaveChangesPipelineBuilder, BeforeSaveChangesPipelineBuilder>();
    }
}