using System.ComponentModel.DataAnnotations;

namespace AsistenciaApp.Modules.Asistencias.Dtos;

public class CrearAsistenciaDto
{
    [Required(ErrorMessage = "El codigo del empleado es obligatorio.")]
    [stringLength(20, Minimumlegth = 3, ErrorMessage = "El codigo debe tener entre 3 y 20 caracteres.")]
    public string CodigoEmpleado { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del empleado es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string NombreEmpleado { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateOnly? Fecha { get; set; }

    [Required(ErrorMessage = "La hora de entrada es obligatoria.")]
    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    [StringLength(250, ErrorMessage = "La observación no puede exceder los 250 caracteres.")]
    public string? Observacion { get; set; }
}