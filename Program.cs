using AsistenciaApp.Data;
using AsistenciaApp.Modules.Asistencias;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnection"));
});

builder.Services.AddControllers();
builder.Services.AddAsistenciasModule();

var app = builder.Build();

app.MapControllers();

app.Run("http://localhost:3000");