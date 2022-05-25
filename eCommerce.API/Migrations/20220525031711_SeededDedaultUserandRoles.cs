using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.API.Migrations
{
    public partial class SeededDedaultUserandRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665", "b3feca3c-ca76-435b-a7e1-e418fcc8be94", "Administrator", "ADMINISTRATOR" },
                    { "b2ae64a2-e75d-471b-9beb-3ad2051100a7", "2a8d5e7a-44ec-466e-916f-47122a0eee48", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "firstName", "isActive", "lastName" },
                values: new object[,]
                {
                    { "2f6d6dd3-d5c5-47e6-958f-f0e132155645", 0, "624792fa-e4ca-470d-a268-64c2ad3a447e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@ecommerce.com", false, false, null, "ADMIN@ECOMMERCE.COM", "ADMIN@ECOMMERCE.COM", "AQAAAAEAACcQAAAAEFo10ebrXFZyMHJEFNk8hPYzX6HCzmgCFETbSdQ2MxIHbCw6ezdnyWSPiekm6/j6BA==", null, false, "21346450-97af-43e0-ab53-c1921e30cb30", false, "admin@ecommerce.com", "System", false, "Admin" },
                    { "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6", 0, "2f60504a-1eaf-46a8-8f74-e085340dd9e1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@ecommerce.com", false, false, null, "USER@ECOMMERCE.COM", "USER@ECOMMERCE.COM", "AQAAAAEAACcQAAAAEDN5nHTQnDvQbrSYXJGdWoIsHRa7YGiMAXh1kJa+gmn+eFxqKiv4BAs5zXZh4ogsFg==", null, false, "85eb2bb7-9bfd-436b-876e-5edb900b1bea", false, "user@ecommerce.com", "System", false, "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665", "2f6d6dd3-d5c5-47e6-958f-f0e132155645" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b2ae64a2-e75d-471b-9beb-3ad2051100a7", "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665", "2f6d6dd3-d5c5-47e6-958f-f0e132155645" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b2ae64a2-e75d-471b-9beb-3ad2051100a7", "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2ae64a2-e75d-471b-9beb-3ad2051100a7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f6d6dd3-d5c5-47e6-958f-f0e132155645");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6");
        }
    }
}
