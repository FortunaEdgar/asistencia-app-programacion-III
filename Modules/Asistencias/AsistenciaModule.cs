using AsistenciaApp.Modules.Asistencias.Repositories;
using AsistenciaApp.Modules.Asistencias.Services;

namespace AsistenciaApp.Modules.Asistencias;

public static class AsistenciaModule
{
    public static IServiceCollection AddAsistenciasModule(this IServiceCollection services)
    {
        // Scoped: el DbContext vive por petición, así que repositorio y servicio también
        services.AddScoped<AsistenciaRepository>();
        services.AddScoped<AsistenciaService>();
        return services;
    }
}
