using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class migrationForUserSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "772848a0-3b20-46bd-8fd3-d83d110aecf0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2203a835-261f-4cf2-a648-67707a980a50", "AQAAAAEAACcQAAAAEDSNWPkwgSXGANYBDpXrigai2qmyocSzUpY60+5uKXOLi0yZScfi4WKZth+tHdcaiQ==", "09346662-283d-4736-a35b-114405f44351" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "772848a0-3b20-46bd-8fd3-d83d110aecf0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c28897aa-4b0e-4374-881a-e629b34cdf79", "AQAAAAEAACcQAAAAEGBcfMQzJEwiIGEU+7kFgbvOHPA3LhCcDtfd/XOlVTQDephPdjrRpi/+p+2y5JfRLQ==", "0e3d13c4-8d62-4e32-bea6-7edde2fc97e6" });
        }
    }
}
