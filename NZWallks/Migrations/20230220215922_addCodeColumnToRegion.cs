using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWallks.Migrations
{
    public partial class addCodeColumnToRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Regions");
        }
    }
}
