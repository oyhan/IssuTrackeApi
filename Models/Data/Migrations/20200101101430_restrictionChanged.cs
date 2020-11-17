using Microsoft.EntityFrameworkCore.Migrations;

namespace Asanobat.IssueTracker.Models.Data.Migrations
{
    public partial class restrictionChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        

            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "8844bce0-f325-4dbc-b2cd-c0c3daa05a3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "8930b4c7-0abf-4a4f-b2bc-b873f2d987e6", "ADMIN@POOYANSYSTEM.COM", "AQAAAAEAACcQAAAAEBGGGBcE7Bbl0KGdcB++3+RQUgoPPusWuvIlals5teV+yZvGSrpBDQByVEyDQAhyOQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys",
                column: "IssueTypeId",
                principalTable: "IssuesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "fc97801c-cdc6-4f78-83b4-b7b10ce1f50f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "c9a1cdc4-2e3f-4ddf-a84d-5a8257a16161", "some-admin-email@nonce.fake", "AQAAAAEAACcQAAAAEMTbDhHEM6uM4jPf7iJxmkv2kzb07a5wbwRzqgGG5zw4lxxXk/qbhjApEmPi0ak/Jw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys",
                column: "IssueTypeId",
                principalTable: "IssuesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
