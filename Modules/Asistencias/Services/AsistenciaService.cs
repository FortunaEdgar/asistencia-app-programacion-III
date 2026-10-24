using AsistenciaApp.Modules.Asistencias.Dtos;
using AsistenciaApp.Modules.Asistencias.Entities;

namespace AsistenciaApp.Modules.Asistencias.Services;

public class AsistenciaService
{
    private static readonly TimeOnly HoraOficialEntrada = new(8, 0);
    private const int MinutosTolerancia = 10;

    private readonly List<Asistencia> _asistencias = [
        new Asistencia {
            CodigoEmpleado = "EMP-001",
            NombreEmpleado = "Ana Martinez",
            Fecha = new DateOnly(2026, 10, 19),
            HoraEntrada = new TimeOnly(7, 55),
            HoraSalida = new TimeOnly(17, 0),
            Estado = "Presente"
        },
        new Asistencia {
            CodigoEmpleado = "EMP-002",
            NombreEmpleado = "Luis Perez",
            Fecha = new DateOnly(2026, 10, 19),
            HoraEntrada = new TimeOnly(8, 25),
            Estado = "Tardanza",
            Observacion = "Problemas de transporte"
        }
       ];
    public List<Asistencia> FindAll()
    {
        return _asistencias;
    }

    public Asistencia? FindOne(Guid id)
    {
        return _asistencias.FirstOrDefault(a => a.Id == id);
    }

    public bool ExisteRegistro(string codigoEmpleado, DateOnly fecha, Guid? excluidId = null)
    {
        return _asistencias.Any(a => a.CodigoEmpleado.Equals(codigoEmpleado.Trim(), StringComparison.OrdinalIgnoreCase) && a.Fecha == fecha && a.Id != excluidId);
    }

    public Asistencia Create(CrearAsistenciaDto dto)
    {
        var asistencia = new Asistencia
        {
            CodigoEmpleado = dto.CodigoEmpleado.Trim(),
            NombreEmpleado = dto.NombreEmpleado.Trim(),
            Fecha = dto.Fecha!.Value,
            HoraEntrada = dto.HoraEntrada!.Value,
            HoraSalida = dto.HoraSalida,
            Observacion = dto.Observacion?.Trim(),
            Estado = CalcularEstado(dto.HoraEntrada!.Value)
        };

        _asistencias.Add(asistencia);
        return asistencia;
    }

    public Asistencia? Update(Guid id, ActualizarAsistenciaDto dto)
    {
        var asistencia = FindOne(id);
        if (asistencia is null) return null;

        if (dto.CodigoEmpleado is not null) asistencia.CodigoEmpleado = dto.CodigoEmpleado.Trim();
        if (dto.NombreEmpleado is not null) asistencia.NombreEmpleado = dto.NombreEmpleado.Trim();
        if (dto.Fecha is not null) asistencia.Fecha = dto.Fecha.Value;
        if (dto.HoraSalida is not null) asistencia.HoraSalida = dto.HoraSalida;
        if (dto.Observacion is not null) asistencia.Observacion = dto.Observacion.Trim();
        if (dto.HoraEntrada is not null)
        {
            asistencia.HoraEntrada = dto.HoraEntrada.Value;
            asistencia.Estado = CalcularEstado(dto.HoraEntrada.Value);
        }

        return asistencia;
    }

    public Asistencia? Remove(Guid id)
    {
        var asistencia = FindOne(id);
        if (asistencia is null) return null;

        _asistencias.Remove(asistencia);
        return asistencia;
    }

    private static string CalcularEstado(TimeOnly horaEntrada)
    {
        var limite = HoraOficialEntrada.AddMinutes(MinutosTolerancia);
        return horaEntrada <= limite ? "Presente" : "Tardanza";
    }
}