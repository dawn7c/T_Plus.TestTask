using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Plus.ThermalProgram.Migrations
{
    /// <inheritdoc />
    public partial class thrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_ThermalNodes",
                table: "ThermalNodes",
                column: "ThermalNodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThermalNodes",
                table: "ThermalNodes");
        }
    }
}
