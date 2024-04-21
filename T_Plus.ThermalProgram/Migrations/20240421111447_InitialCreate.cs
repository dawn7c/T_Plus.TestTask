using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Plus.ThermalProgram.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThermalNodes",
                columns: table => new
                {
                    ThermalNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThermalNodeName = table.Column<string>(type: "text", nullable: false),
                    RepairCost = table.Column<double>(type: "double precision", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThermalNodes", x => x.ThermalNodeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThermalNodes");
        }
    }
}
