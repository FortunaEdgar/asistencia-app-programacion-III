using AsistenciaApp.Modules.Asistencias.Services;

namespace AsistenciaApp.Modules.Asistencias;

public static class AsistenciaModule
{
    public static IServiceCollection AddAsistenciasModule(this IServiceCollection services)
    {
        services.AddSingleton<AsistenciaService>();
        return services;
    }
}