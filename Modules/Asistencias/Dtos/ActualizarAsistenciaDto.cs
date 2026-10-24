using System.ComponentModel.DataAnnotations;

namespace AsistenciaApp.Modules.Asistencias.Dtos;

public class ActualizarAsistenciaDto
{
    [StringLength(20, MinimumLength = 3, ErrorMessage = "El codigo debe tener entre 3 y 20 caracteres.")]
    public string? CodigoEmpleado { get; set; }

    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string? NombreEmpleado { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    [StringLength(250, ErrorMessage = "La observación no puede exceder los 250 caracteres.")]
    public string? Observacion { get; set; }

    public bool EstaVacio() =>
        CodigoEmpleado is null && NombreEmpleado is null && Fecha is null && HoraEntrada is null && HoraSalida is null && Observacion is null;
}