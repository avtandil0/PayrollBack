using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class changeCurrencyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "036f0246-b844-4471-b450-4c4eef779302", "772848a0-3b20-46bd-8fd3-d83d110aecf0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "036f0246-b844-4471-b450-4c4eef779302");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "772848a0-3b20-46bd-8fd3-d83d110aecf0");

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "EmployeeComponents",
                type: "int",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<bool>(
                name: "isPermanent",
                table: "EmployeeComponents",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompCode",
                table: "Calculation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBalance",
                table: "Calculation",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_report_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hrcomp_trans_id = table.Column<int>(type: "int", nullable: true),
                    personalNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    address1 = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    res_id = table.Column<int>(type: "int", nullable: false),
                    payrollYear = table.Column<int>(type: "int", nullable: false),
                    period = table.Column<int>(type: "int", nullable: false),
                    calculationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    comp_code = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    pension_schema = table.Column<int>(type: "int", nullable: true),
                    base_value = table.Column<double>(type: "float", nullable: false),
                    issued_amount = table.Column<double>(type: "float", nullable: false),
                    grace_value = table.Column<double>(type: "float", nullable: false),
                    income_tax = table.Column<double>(type: "float", nullable: false),
                    initial_grace = table.Column<double>(type: "float", nullable: false),
                    remaining_grace = table.Column<double>(type: "float", nullable: false),
                    land_iso = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    land_isonr = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    oms60_0 = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    departmentID = table.Column<int>(type: "int", nullable: true),
                    departmentName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    startTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsBreakTime = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateChange = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK__TimePerio__Emplo__14270015",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WeekDay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WorkingStartTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WorkingEndTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BreakingStartTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BreakingEndTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateChange = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateChange = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Rates__CurrencyI__43D61337",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CurrencyId",
                table: "Rates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriods_EmployeeId",
                table: "TimePeriods",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payroll_report_data");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropColumn(
                name: "isPermanent",
                table: "EmployeeComponents");

            migrationBuilder.DropColumn(
                name: "CompCode",
                table: "Calculation");

            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "Calculation");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "EmployeeComponents",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "036f0246-b844-4471-b450-4c4eef779302", "036f0246-b844-4471-b450-4c4eef779302", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateChanged", "DateCreated", "DateDeleted", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "772848a0-3b20-46bd-8fd3-d83d110aecf0", 0, "2203a835-261f-4cf2-a648-67707a980a50", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "aa@aa.ge", true, "avtandil", "zenai", false, null, null, null, "AQAAAAEAACcQAAAAEDSNWPkwgSXGANYBDpXrigai2qmyocSzUpY60+5uKXOLi0yZScfi4WKZth+tHdcaiQ==", null, false, "09346662-283d-4736-a35b-114405f44351", false, "26001037" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "036f0246-b844-4471-b450-4c4eef779302", "772848a0-3b20-46bd-8fd3-d83d110aecf0" });
        }
    }
}
