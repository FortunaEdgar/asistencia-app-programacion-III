using AsistenciaApp.Modules.Asistencias;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAsistenciasModule();

var app = builder.Build();

app.MapControllers();

app.Run("http://localhost:3000");
