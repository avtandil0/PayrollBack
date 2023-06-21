using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class compTypeInCaltion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompType",
                table: "Calculation",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompType",
                table: "Calculation");
        }
    }
}
