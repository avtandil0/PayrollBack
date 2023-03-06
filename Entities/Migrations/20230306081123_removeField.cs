using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class removeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency100",
                table: "EmployeeComponents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency100",
                table: "EmployeeComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
