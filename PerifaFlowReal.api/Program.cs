using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PerifaFlowReal.api.extensions;
using PerifaFlowReal.Application;
using PerifaFlowReal.Application.Configs;
using PerifaFlowReal.Application.Interfaces.Services.JWT;
using PerifaFlowReal.Infastructure;

namespace PerifaFlowReal.api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var configs = builder.Configuration.Get<Settings>();
        
        // Add services to the container.
        builder.Services.AddSingleton(configs);
        builder.Services.AddSingleton(configs.Jwt);
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger(configs);
        
        builder.Services.AddInfra(configs);
        builder.Services.AddUseCases();
        builder.Services.AddScoped<ITokenService,  TokenService>();
        
        builder.Services.AddHealthServices(builder.Configuration);
        
        builder.Services.AddVersioning();
        
        builder.Services.AddVerifyJwt(configs.Jwt);
        
        var app = builder.Build();
        
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1/swagger.json",  "PerifaFlow.API v1");
                ui.SwaggerEndpoint("/swagger/v2/swagger.json",  "PerifaFlow.API v2");
            });
        }

        app.UseRouting();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse
            });
        });

        app.MapControllers();

        app.Run();
    }
}