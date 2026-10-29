using AsistenciaApp.Modules.Asistencias.Dtos;
using AsistenciaApp.Modules.Asistencias.Entities;
using AsistenciaApp.Modules.Asistencias.Repositories;

namespace AsistenciaApp.Modules.Asistencias.Services;

public class AsistenciaService(AsistenciaRepository asistenciaRepository)
{
    private static readonly TimeOnly HoraOficialEntrada = new(8, 0);
    private const int MinutosTolerancia = 10;

    private readonly AsistenciaRepository _asistenciaRepository = asistenciaRepository;

    public List<Asistencia> FindAll()
    {
        return _asistenciaRepository.FindAll();
    }

    public Asistencia? FindOne(Guid id)
    {
        return _asistenciaRepository.FindOne(id);
    }

    public bool ExisteRegistro(string codigoEmpleado, DateOnly fecha, Guid? excluirId = null)
    {
        return _asistenciaRepository.ExisteRegistro(codigoEmpleado, fecha, excluirId);
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

        _asistenciaRepository.Create(asistencia);
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

        _asistenciaRepository.Update(asistencia);
        return asistencia;
    }

    public Asistencia? Remove(Guid id)
    {
        var asistencia = FindOne(id);
        if (asistencia is null) return null;

        _asistenciaRepository.Remove(asistencia);
        return asistencia;
    }

    private static string CalcularEstado(TimeOnly horaEntrada)
    {
        var limite = HoraOficialEntrada.AddMinutes(MinutosTolerancia);
        return horaEntrada <= limite ? "Presente" : "Tardanza";
    }
}
