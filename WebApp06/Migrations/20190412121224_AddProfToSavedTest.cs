using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class AddProfToSavedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessorId",
                table: "SavedTests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedTests_ProfessorId",
                table: "SavedTests",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedTests_AspNetUsers_ProfessorId",
                table: "SavedTests",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedTests_AspNetUsers_ProfessorId",
                table: "SavedTests");

            migrationBuilder.DropIndex(
                name: "IX_SavedTests_ProfessorId",
                table: "SavedTests");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "SavedTests");
        }
    }
}
