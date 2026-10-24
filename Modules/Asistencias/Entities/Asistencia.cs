namespace AsistenciaApp.Modules.Asistencias.Entities;

public class Asistencia
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string CodigoEmpleado { get; set; }
    public required string NombreEmpleado { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraEntrada { get; set; }
    public TimeOnly? HoraSalida { get; set; }
    public required string Estado { get; set; }
}