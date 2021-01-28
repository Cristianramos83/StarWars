using Microsoft.EntityFrameworkCore.Migrations;

namespace Satellite.Persistence.Database.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Satellite");

            migrationBuilder.CreateTable(
                name: "Satellites",
                schema: "Satellite",
                columns: table => new
                {
                    SatelliteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    CoordinateX = table.Column<double>(type: "float", nullable: false),
                    CoordinateY = table.Column<double>(type: "float", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.SatelliteId);
                });

            migrationBuilder.InsertData(
                schema: "Satellite",
                table: "Satellites",
                columns: new[] { "SatelliteId", "CoordinateX", "CoordinateY", "Distance", "Message", "Name" },
                values: new object[] { 1, -500.0, -200.0, 0.0, null, "Kenobi" });

            migrationBuilder.InsertData(
                schema: "Satellite",
                table: "Satellites",
                columns: new[] { "SatelliteId", "CoordinateX", "CoordinateY", "Distance", "Message", "Name" },
                values: new object[] { 2, -100.0, -100.0, 0.0, null, "Skywalker" });

            migrationBuilder.InsertData(
                schema: "Satellite",
                table: "Satellites",
                columns: new[] { "SatelliteId", "CoordinateX", "CoordinateY", "Distance", "Message", "Name" },
                values: new object[] { 3, 500.0, 100.0, 0.0, null, "Sato" });

            migrationBuilder.CreateIndex(
                name: "IX_Satellites_SatelliteId",
                schema: "Satellite",
                table: "Satellites",
                column: "SatelliteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satellites",
                schema: "Satellite");
        }
    }
}
