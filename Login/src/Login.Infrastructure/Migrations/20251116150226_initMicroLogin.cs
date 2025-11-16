using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Login.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initMicroLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "altura_cm",
                table: "Alumno");

            migrationBuilder.DropColumn(
                name: "peso_kg",
                table: "Alumno");

            migrationBuilder.AddColumn<decimal>(
                name: "altura",
                table: "Usuario",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "peso",
                table: "Usuario",
                type: "numeric(5,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "altura",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "peso",
                table: "Usuario");

            migrationBuilder.AddColumn<decimal>(
                name: "altura_cm",
                table: "Alumno",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "peso_kg",
                table: "Alumno",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
