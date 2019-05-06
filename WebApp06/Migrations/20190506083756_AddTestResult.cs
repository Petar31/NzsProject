using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class AddTestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

           

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    StudentId = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Result = table.Column<double>(nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => new { x.StudentId, x.TestId });
                    table.ForeignKey(
                        name: "FK_TestResults_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_SavedTests_TestId",
                        column: x => x.TestId,
                        principalTable: "SavedTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_StudentId",
                table: "TestResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestId",
                table: "TestResults",
                column: "TestId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "TestResults");

           
        }
    }
}
