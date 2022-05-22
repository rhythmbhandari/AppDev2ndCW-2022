using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev2ndCW_2022.Migrations
{
    public partial class UpdateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1755868f-53b8-4525-b097-99172abb1835");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa877c6a-1f53-48dd-bc8e-6cd249af222b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c35bef2a-cdc9-4c96-ad37-3a0ce867b6ce", "e293214f-d0d3-4645-bf78-e0eea8cbae4d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc58b243-d840-495f-b86a-4d297e75ad01", "2cd21783-de87-4701-ba3e-fc93f2db4228", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c35bef2a-cdc9-4c96-ad37-3a0ce867b6ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc58b243-d840-495f-b86a-4d297e75ad01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1755868f-53b8-4525-b097-99172abb1835", "3d197ae9-0a94-43d6-8276-62fdc8ac3f87", "Manager", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa877c6a-1f53-48dd-bc8e-6cd249af222b", "72265cd1-6419-49bf-8332-0a73d17c2213", "Admin", "ADMIN" });
        }
    }
}
