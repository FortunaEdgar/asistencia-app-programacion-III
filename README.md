# AsistenciaApp

Web API en **ASP.NET Core** para gestionar la asistencia diaria de empleados (entrada, salida y puntualidad).

Proyecto de **Lenguaje de Programación III** – UFHEC · Edgar Antonio Yan Fortuna

## Estructura

```
Modules/Asistencias/
 ├── Controllers/
 ├── Dtos/
 ├── Entities/
 ├── Services/
 └── AsistenciasModule.cs
```

## Endpoints

Base: `http://localhost:3000/api/asistencias`

| Método | Ruta | Respuestas |
|---|---|---|
| GET | `/` | 200 |
| GET | `/{id}` | 200, 404 |
| POST | `/` | 201, 400 |
| PATCH | `/{id}` | 200, 400, 404 |
| DELETE | `/{id}` | 200, 404 |

El estado (`Presente` / `Tardanza`) se calcula automáticamente: la entrada hasta las 8:10 cuenta como puntual.

## Ejecutar

```cmd
dotnet run
```