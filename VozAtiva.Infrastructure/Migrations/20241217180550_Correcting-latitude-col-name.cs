using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VozAtiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correctinglatitudecolname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "alert",
                newName: "Longitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "alert",
                newName: "Longitute");
        }
    }
}
