using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class grceAmountPercent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GraceAmountPercent",
                table: "Employee",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraceAmountPercent",
                table: "Employee");
        }
    }
}
