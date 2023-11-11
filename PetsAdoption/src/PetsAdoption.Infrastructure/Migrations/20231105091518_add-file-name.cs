using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsAdoption.Infrastructure.Migrations
{
    public partial class addfilename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Pictures");
        }
    }
}
