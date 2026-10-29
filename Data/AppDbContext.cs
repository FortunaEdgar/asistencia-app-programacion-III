using AsistenciaApp.Modules.Asistencias.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaApp.Data;

public class  AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) { }
{
    public DbSet<Asistencia> Asistencias => Set<Asistencia>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Asistencia>(entity =>
    {
        entity.HasKey(a => a.Id);

        entity.Property(a => a.CodigoEmpleado).HasMaxLength(20).IsRequired();
        entity.Property(a => a.NombreEmpleado).HasMaxLength(100).IsRequired();
        entity.Property(a => a.Estado).HasMaxLength(20).IsRequired();
        entity.Property(a => a.Observacion).HasMaxLength(250);

        entity.HasIndex(a => new { a.CodigoEmpleado, a.Fecha }).IsUnique();

        entity.HasData(
            new Asistencia
            {
                Id = Guid.Parse("8f1c2b7a-3d4e-4f5a-9b6c-1a2b3c4d5e01"),
                CodigoEmpleado = "EMP-001",
                NombreEmpleado = "Ana Martinez",
                Fecha = new DateOnly(2026, 10, 19),
                HoraEntrada = new TimeOnly(7, 55),
                HoraSalida = new TimeOnly(17, 0),
                Estado = "Presente"
            },
            new Asistencia
            {
                Id = Guid.Parse("8f1c2b7a-3d4e-4f5a-9b6c-1a2b3c4d5e02"),
                CodigoEmpleado = "EMP-002",
                NombreEmpleado = "Luis Perez",
                Fecha = new DateOnly(2026, 10, 19),
                HoraEntrada = new TimeOnly(8, 25),
                Estado = "Tardanza",
                Observacion = "Problemas de transporte"
            }
        );
    });
 }