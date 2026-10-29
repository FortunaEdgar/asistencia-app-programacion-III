using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AsistenciaApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NombreEmpleado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraEntrada = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HoraSalida = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Observacion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Asistencias",
                columns: new[] { "Id", "CodigoEmpleado", "Estado", "Fecha", "HoraEntrada", "HoraSalida", "NombreEmpleado", "Observacion" },
                values: new object[,]
                {
                    { new Guid("8f1c2b7a-3d4e-4f5a-9b6c-1a2b3c4d5e01"), "EMP-001", "Presente", new DateOnly(2026, 10, 19), new TimeOnly(7, 55, 0), new TimeOnly(17, 0, 0), "Ana Martinez", null },
                    { new Guid("8f1c2b7a-3d4e-4f5a-9b6c-1a2b3c4d5e02"), "EMP-002", "Tardanza", new DateOnly(2026, 10, 19), new TimeOnly(8, 25, 0), null, "Luis Perez", "Problemas de transporte" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_CodigoEmpleado_Fecha",
                table: "Asistencias",
                columns: new[] { "CodigoEmpleado", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");
        }
    }
}
