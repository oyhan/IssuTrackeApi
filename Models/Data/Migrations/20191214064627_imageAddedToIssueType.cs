using Microsoft.EntityFrameworkCore.Migrations;

namespace Asanobat.IssueTracker.Models.Data.Migrations
{
    public partial class imageAddedToIssueType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "IssuesTypes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "IssuesTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Issues_IssueId",
                table: "PropertyValues",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
