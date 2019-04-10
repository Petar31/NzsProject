using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class AddQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Context = table.Column<string>(maxLength: 50, nullable: true),
                    CorrectAnswer = table.Column<string>(maxLength: 40, nullable: true),
                    Grade = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    WrongAnswer1 = table.Column<string>(maxLength: 40, nullable: true),
                    WrongAnswer2 = table.Column<string>(maxLength: 40, nullable: true),
                    WrongAnswer3 = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "Question");

           
        }
    }
}
