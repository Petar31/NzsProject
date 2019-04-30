using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class AddSubjectToTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "SavedTests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SavedTests_SubjectId",
                table: "SavedTests",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedTests_Subjects_SubjectId",
                table: "SavedTests",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedTests_Subjects_SubjectId",
                table: "SavedTests");

            migrationBuilder.DropIndex(
                name: "IX_SavedTests_SubjectId",
                table: "SavedTests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SavedTests");
        }
    }
}
