using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Login.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueEmailConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { new Guid("443423fb-a792-42c5-87d7-030f081a4c89"), "Alumno" },
                    { new Guid("62da6d8c-0789-40e8-b889-fead572d9994"), "Admin" },
                    { new Guid("c8ae1137-7c73-4350-a1b5-2eaf3beece99"), "Entrenador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("443423fb-a792-42c5-87d7-030f081a4c89"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("62da6d8c-0789-40e8-b889-fead572d9994"));

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: new Guid("c8ae1137-7c73-4350-a1b5-2eaf3beece99"));

            migrationBuilder.AlterColumn<bool>(
                name: "activo",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

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
    }
}
