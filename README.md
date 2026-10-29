# AsistenciaApp

Web API en **ASP.NET Core** para gestionar la asistencia diaria de empleados (entrada, salida y puntualidad).

Proyecto de **Lenguaje de Programación III** – UFHEC · Edgar Antonio Yan Fortuna

## Estructura

```
Data/
 └── AppDbContext.cs          # DbContext de EF Core (PostgreSQL)
Migrations/                   # Migraciones generadas por EF Core
Modules/Asistencias/
 ├── Controllers/             # Endpoints HTTP y validaciones
 ├── Dtos/
 ├── Entities/
 ├── Repositories/            # Acceso a datos con AppDbContext
 ├── Services/                # Lógica de negocio (cálculo del estado)
 └── AsistenciaModule.cs      # Registro de dependencias (Scoped)
```

Flujo: `Controller → Service → Repository → AppDbContext → PostgreSQL`

## Base de datos

Paquetes usados:

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Contenedor de PostgreSQL:

```shell
docker run --name asistencia-postgres -e POSTGRES_PASSWORD=root -e POSTGRES_USER=root -e POSTGRES_DB=asistencia_db -dp 5435:5432 postgres:18
```

Cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "postgresConnection": "Host=localhost;Port=5435;Database=asistencia_db;Username=root;Password=root"
}
```

Migraciones:

```shell
dotnet ef migrations add InitialCreate
dotnet ef database update
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

## Pruebas con Postman

La colección `postman/AsistenciaApp.postman_collection.json` tiene 16 peticiones con pruebas automáticas, organizadas en las carpetas Consultas, Crear, Actualizar y Eliminar. Cubre los casos válidos y las validaciones: duplicados, fecha futura, hora de salida, campos obligatorios y registros inexistentes.

1. Levantar la API con `dotnet run`.
2. Importar la colección en Postman.
3. Ejecutarla completa y en orden con el **Collection Runner**: el POST guarda el `id` que usan PATCH y DELETE.

También se puede ejecutar desde la terminal con Newman:

```cmd
npx newman run postman/AsistenciaApp.postman_collection.json
```
