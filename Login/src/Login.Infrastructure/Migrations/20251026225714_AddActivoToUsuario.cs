using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Login.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivoToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("84ca0972-519a-4207-81cf-2912595a88fa"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("abbeee1d-f039-4349-8a3f-456a90c76838"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("d13e9d2f-f4c9-446e-a69d-3c1c5db142b6"));

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { new Guid("68f0307c-5ece-4403-8697-b4da6645db09"), "Entrenador" },
                    { new Guid("7aceaa8e-8756-4be1-9aa6-f497ad0f27cc"), "Admin" },
                    { new Guid("e6b8eeb1-0d81-4aa3-a02d-ecf40de046ad"), "Alumno" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("68f0307c-5ece-4403-8697-b4da6645db09"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("7aceaa8e-8756-4be1-9aa6-f497ad0f27cc"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("e6b8eeb1-0d81-4aa3-a02d-ecf40de046ad"));

            migrationBuilder.DropColumn(
                name: "activo",
                table: "Usuario");

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { new Guid("84ca0972-519a-4207-81cf-2912595a88fa"), "Admin" },
                    { new Guid("abbeee1d-f039-4349-8a3f-456a90c76838"), "Alumno" },
                    { new Guid("d13e9d2f-f4c9-446e-a69d-3c1c5db142b6"), "Entrenador" }
                });
        }
    }
}
