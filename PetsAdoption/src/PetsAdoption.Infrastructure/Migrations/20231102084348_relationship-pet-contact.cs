using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsAdoption.Infrastructure.Migrations
{
    public partial class relationshippetcontact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId1",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ContactId",
                table: "Pets",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ContactId1",
                table: "Pets",
                column: "ContactId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Contacts_ContactId",
                table: "Pets",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Contacts_ContactId1",
                table: "Pets",
                column: "ContactId1",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Contacts_ContactId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Contacts_ContactId1",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_ContactId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_ContactId1",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "Pets");
        }
    }
}
