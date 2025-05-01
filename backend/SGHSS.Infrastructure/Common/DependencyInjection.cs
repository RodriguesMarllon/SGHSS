using Domain.Interfaces.AppSettings;
using Domain.Interfaces.Repositories.Pacientes;
using Domain.Interfaces.Requests;
using Domain.Service.Pacientes;
using Infrastructure.Repositories.Pacientes;
using Infrastructure.Services.AppSettings;
using Infrastructure.Services.Requests;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Common;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {   
        services.AddScoped<IPacienteRepository, PacienteRepository>();

        services.AddTransient<IPacienteService, PacienteService>();

        services.AddTransient<IApiSettingsService, ApiSettingsService>();
        services.AddTransient<IExternalApiService, ExternalApiService>();

        return services;
    }
}
