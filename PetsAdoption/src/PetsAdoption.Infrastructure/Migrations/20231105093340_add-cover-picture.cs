using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsAdoption.Infrastructure.Migrations
{
    public partial class addcoverpicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "Pictures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "Pictures");
        }
    }
}
