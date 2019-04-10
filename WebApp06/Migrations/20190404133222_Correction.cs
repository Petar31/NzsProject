using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            //migrationBuilder.DropTable(
            //    name: "SubjectRole");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Question",
            //    table: "Question");

            //migrationBuilder.RenameTable(
            //    name: "Question",
            //    newName: "Questions");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Question_SubjectId",
            //    table: "Questions",
            //    newName: "IX_Questions_SubjectId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Questions",
            //    table: "Questions",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "Professor",
            //    columns: table => new
            //    {
            //        SubjectId = table.Column<int>(nullable: false),
            //        UserId = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Professor", x => x.SubjectId);
            //        table.ForeignKey(
            //            name: "FK_Professor_Subjects_SubjectId",
            //            column: x => x.SubjectId,
            //            principalTable: "Subjects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Professor_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Professor_UserId",
            //    table: "Professor",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Questions_Subjects_SubjectId",
            //    table: "Questions",
            //    column: "SubjectId",
            //    principalTable: "Subjects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Questions_Subjects_SubjectId",
            //    table: "Questions");

            //migrationBuilder.DropTable(
            //    name: "Professor");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Questions",
            //    table: "Questions");

            //migrationBuilder.RenameTable(
            //    name: "Questions",
            //    newName: "Question");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Questions_SubjectId",
            //    table: "Question",
            //    newName: "IX_Question_SubjectId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Question",
            //    table: "Question",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "SubjectRole",
            //    columns: table => new
            //    {
            //        SubjectId = table.Column<int>(nullable: false),
            //        RoleId = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SubjectRole", x => x.SubjectId);
            //        table.ForeignKey(
            //            name: "FK_SubjectRole_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SubjectRole_Subjects_SubjectId",
            //            column: x => x.SubjectId,
            //            principalTable: "Subjects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubjectRole_RoleId",
            //    table: "SubjectRole",
            //    column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Question_Subjects_SubjectId",
            //    table: "Question",
            //    column: "SubjectId",
            //    principalTable: "Subjects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
