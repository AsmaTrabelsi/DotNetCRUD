using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWallks.Migrations
{
    public partial class updateRegionColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Regions",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Regions",
                newName: "Code");
        }
    }
}
