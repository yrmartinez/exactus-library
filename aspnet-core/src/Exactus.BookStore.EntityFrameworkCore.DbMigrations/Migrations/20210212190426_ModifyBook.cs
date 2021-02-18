using Microsoft.EntityFrameworkCore.Migrations;

namespace Exactus.BookStore.Migrations
{
    public partial class ModifyBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "AppBooks",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "AppBooks");
        }
    }
}
