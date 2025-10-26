using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Login.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetNullOnEntrenadorDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumno_Usuario_entrenador_id",
                table: "Alumno");

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("01825310-d36a-4307-a2e9-80469ae74e61"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("4ff5e8ba-4a98-437e-87cb-eb80616dd1b9"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("c59c1878-1e06-4542-9f1a-e28c33fbcfe3"));

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { new Guid("84ca0972-519a-4207-81cf-2912595a88fa"), "Admin" },
                    { new Guid("abbeee1d-f039-4349-8a3f-456a90c76838"), "Alumno" },
                    { new Guid("d13e9d2f-f4c9-446e-a69d-3c1c5db142b6"), "Entrenador" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Alumno_Usuario_entrenador_id",
                table: "Alumno",
                column: "entrenador_id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumno_Usuario_entrenador_id",
                table: "Alumno");

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

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { new Guid("01825310-d36a-4307-a2e9-80469ae74e61"), "Admin" },
                    { new Guid("4ff5e8ba-4a98-437e-87cb-eb80616dd1b9"), "Alumno" },
                    { new Guid("c59c1878-1e06-4542-9f1a-e28c33fbcfe3"), "Entrenador" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Alumno_Usuario_entrenador_id",
                table: "Alumno",
                column: "entrenador_id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
