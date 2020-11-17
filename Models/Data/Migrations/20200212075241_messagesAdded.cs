using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asanobat.IssueTracker.Models.Data.Migrations
{
    public partial class messagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true),
                    IssueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageModel_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageModel_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageModel_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "0f1806c1-6f68-4a5d-b2f7-2408c57c33de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3112b41-542f-4064-af1e-1b772b7c71e3", "AQAAAAEAACcQAAAAEC7t969CSJ8bYp05whUywgMV9tfzYoZCrZiJo6yijrdwUsLEru4JyxjNmIUeniGs7g==" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_IssueId",
                table: "MessageModel",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_ReceiverId",
                table: "MessageModel",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_SenderId",
                table: "MessageModel",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageModel");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8930b4c7-0abf-4a4f-b2bc-b873f2d987e6", "AQAAAAEAACcQAAAAEBGGGBcE7Bbl0KGdcB++3+RQUgoPPusWuvIlals5teV+yZvGSrpBDQByVEyDQAhyOQ==" });
        }
    }
}
