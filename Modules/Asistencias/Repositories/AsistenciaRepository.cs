using AsistenciaApp.Data;
using AsistenciaApp.Modules.Asistencias.Entities;

namespace AsistenciaApp.Modules.Asistencias.Repositories;

public class AsistenciaRepository(AppDbContext dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public List<Asistencia> FindAll()
    {
        return _dbContext.Asistencias
            .OrderByDescending(a => a.Fecha)
            .ThenBy(a => a.CodigoEmpleado)
            .ToList();
    }

    public Asistencia? FindOne(Guid id)
    {
        return _dbContext.Asistencias.FirstOrDefault(a => a.Id == id);
    }

    public bool ExisteRegistro(string codigoEmpleado, DateOnly fecha, Guid? excluirId = null)
    {
        var codigo = codigoEmpleado.Trim().ToUpper();

        return _dbContext.Asistencias.Any(a =>
            a.CodigoEmpleado.ToUpper() == codigo &&
            a.Fecha == fecha &&
            a.Id != excluirId);
    }

    public void Create(Asistencia asistencia)
    {
        _dbContext.Asistencias.Add(asistencia);
        _dbContext.SaveChanges();
    }

    public void Update(Asistencia asistencia)
    {
        // La entidad viene de FindOne, por lo que EF ya la está rastreando
        _dbContext.SaveChanges();
    }

    public void Remove(Asistencia asistencia)
    {
        _dbContext.Asistencias.Remove(asistencia);
        _dbContext.SaveChanges();
    }
}
