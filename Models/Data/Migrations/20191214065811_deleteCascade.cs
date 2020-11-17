using Microsoft.EntityFrameworkCore.Migrations;

namespace Asanobat.IssueTracker.Models.Data.Migrations
{
    public partial class deleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys",
                column: "IssueTypeId",
                principalTable: "IssuesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypeProperys_IssuesTypes_IssueTypeId",
                table: "IssueTypeProperys",
                column: "IssueTypeId",
                principalTable: "IssuesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
