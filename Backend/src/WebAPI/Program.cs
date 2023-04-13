using Application;
using Infrastructure;
using WebAPI;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddBeforeSaveChangesPipeline();
builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration, builder.Environment);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddWebApiServices();
builder.Services.AddSwagger();


// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }