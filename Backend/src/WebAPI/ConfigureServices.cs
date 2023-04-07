using System.ComponentModel;
using System.Text.Json.Serialization;
using WebAPI.Middlewares;

namespace WebAPI;

public static class ConfigureServices
{
    public static void AddWebApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddScoped<ExceptionHandlerMiddleware>();
    }
}