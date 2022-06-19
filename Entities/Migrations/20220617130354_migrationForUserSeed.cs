using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class migrationForUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "036f0246-b844-4471-b450-4c4eef779302", "036f0246-b844-4471-b450-4c4eef779302", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateChanged", "DateCreated", "DateDeleted", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "772848a0-3b20-46bd-8fd3-d83d110aecf0", 0, "c28897aa-4b0e-4374-881a-e629b34cdf79", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "aa@aa.ge", true, "avtandil", "zenai", false, null, null, null, "AQAAAAEAACcQAAAAEGBcfMQzJEwiIGEU+7kFgbvOHPA3LhCcDtfd/XOlVTQDephPdjrRpi/+p+2y5JfRLQ==", null, false, "0e3d13c4-8d62-4e32-bea6-7edde2fc97e6", false, "26001037" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "036f0246-b844-4471-b450-4c4eef779302", "772848a0-3b20-46bd-8fd3-d83d110aecf0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
