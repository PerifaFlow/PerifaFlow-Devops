using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerifaFlowReal.Application.Configs;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Application.Interfaces.Services;
using PerifaFlowReal.Infastructure.Java;
using PerifaFlowReal.Infastructure.Percistence.Context;
using PerifaFlowReal.Infastructure.Percistence.Repositories;

namespace PerifaFlowReal.Infastructure;

public static class DependenciInjection
{
    private static IServiceCollection AddDBContext(this IServiceCollection services, ConnectionSettings configuration)
    {
        return services.AddDbContext<PerifaFlowContext>(options =>
        { 
            options.UseSqlServer(configuration.PerifaFlowDb);
        });
    }

    private static IServiceCollection AddServices(this IServiceCollection services, ServiceConnection config)
    {
        services.AddHttpClient<IRitimoService, RitmoService>(client =>
            {
                client.BaseAddress = new Uri(config.BaseUrl);

                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMissaoRepository, MissaoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<ITrilhaRepository, TrilhaRepository>();
        return services;
    }
    
    public static IServiceCollection AddInfra(this IServiceCollection services, Settings settings)
    {
        services.AddDBContext(settings.ConnectionStrings);
        services.AddRepositories();
        services.AddServices(settings.BemEstar);
        return services;
    }

    
}