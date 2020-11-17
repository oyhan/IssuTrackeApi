using Microsoft.EntityFrameworkCore.Migrations;

namespace Asanobat.IssueTracker.Models.Data.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "fc97801c-cdc6-4f78-83b4-b7b10ce1f50f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Family", "Name" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "c9a1cdc4-2e3f-4ddf-a84d-5a8257a16161", "ApplicationUser", "admin@PooyanSystem.com", true, false, null, "some-admin-email@nonce.fake", "ADMIN", "AQAAAAEAACcQAAAAEMTbDhHEM6uM4jPf7iJxmkv2kzb07a5wbwRzqgGG5zw4lxxXk/qbhjApEmPi0ak/Jw==", null, false, "", false, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "fc97801c-cdc6-4f78-83b4-b7b10ce1f50f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "c9a1cdc4-2e3f-4ddf-a84d-5a8257a16161" });
        }
    }
}
