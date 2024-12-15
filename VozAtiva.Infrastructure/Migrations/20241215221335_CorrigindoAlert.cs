using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VozAtiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "alert",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitute",
                table: "alert",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "alert",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "alert");

            migrationBuilder.DropColumn(
                name: "Longitute",
                table: "alert");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "alert");
        }
    }
}
