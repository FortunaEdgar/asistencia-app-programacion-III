using AsistenciaApp.Modules.Asistencias.Dtos;
using AsistenciaApp.Modules.Asistencias.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsistenciaApp.Modules.Asistencias.Controllers;

[ApiController]
[Route("api/asistencias")]
public class AsistenciaController(AsistenciaService asistenciaService) : ControllerBase
{
    private readonly AsistenciaService _asistenciaService = asistenciaService;

    [HttpGet]
    public IActionResult FindAll()
    {
        return Ok(_asistenciaService.FindAll());
    }

    [HttpGet("{id:guid}")]
    public IActionResult FindOne(Guid id)
    {
        var asistencia = _asistenciaService.FindOne(id);

        if ( (asistencia is null))
        {
            return NotFound(new { mensaje = $"No existe una asistencia con el id {id}." });
        }

        return Ok(asistencia);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CrearAsistenciaDto dto)
    {
        if (dto.Fecha > DateOnly.FromDateTime(DateTime.Today))
        {
            return BadRequest(new { mensaje = "No se puede registrar asistencias con fecha futura." });
        }

        if (dto.HoraSalida is not null && dto.HoraSalida <= dto.HoraEntrada)
        {
            return BadRequest(new { mensaje = "La hora de salida debe ser mayor que la hora de entrada." });
        }

        if (_asistenciaService.ExisteRegistro(dto.CodigoEmpleado, dto.Fecha!.Value))
        {
            return BadRequest(new { mensaje = "Ya existe un registro de asistencia para este empleado en la fecha especificada." });
        }

        var nueva = _asistenciaService.Create(dto);

        return CreatedAtAction(nameof(FindOne), new { id = nueva.Id }, nueva);
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ActualizarAsistenciaDto dto)
    {
        if (dto.EstaVacio()) {
            return BadRequest(new { mensaje = "No se proporcionaron campos para actualizar." });
        }

        if (EsTextoEnBlanco(dto.CodigoEmpleado) || EsTextoEnBlanco(dto.NombreEmpleado))
        {
            return BadRequest(new { mensaje = "Los campos CodigoEmpleado y NombreEmpleado no pueden estar vacíos." });
        }

        var actual = _asistenciaService.FindOne(id);
        if (actual is null)
        {
            return NotFound(new { mensaje = $"No existe una asistencia con el id {id}." });
        }

        var fecha = dto.Fecha ?? actual.Fecha;
        var entrada = dto.HoraEntrada ?? actual.HoraEntrada;
        var salida = dto.HoraSalida ?? actual.HoraSalida; 
        var codigo = dto.CodigoEmpleado ?? actual.CodigoEmpleado;

        if (fecha > DateOnly.FromDateTime(DateTime.Today))
        {
            return BadRequest(new { mensaje = "No se puede registrar asistencias con fecha futura." });
        }

        if (salida is not null && salida <= entrada)
        {
            return BadRequest(new { mensaje = "La hora de salida debe ser mayor que la hora de entrada." });
        }

        if (_asistenciaService.ExisteRegistro(codigo, fecha, id))
        {
            return BadRequest(new { mensaje = "Ya existe un registro de asistencia para este empleado en la fecha especificada." });
        }

        var actualizada = _asistenciaService.Update(id, dto);
        return Ok(actualizada);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Remove(Guid id)
    {
        var eliminada = _asistenciaService.Remove(id);

        if (eliminada is null)
        {
            return NotFound(new { mensaje = $"No existe una asistencia con el id {id}." });
        }
        return Ok(new { mensaje = $"Asistencia con id {id} eliminada correctamente." });
    }

    private static bool EsTextoEnBlanco(string? valor) => valor is not null && string.IsNullOrWhiteSpace(valor);
}