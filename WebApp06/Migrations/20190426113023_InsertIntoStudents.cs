using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp06.Migrations
{
    public partial class InsertIntoStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedure = @"create procedure InsertIntoStudents(@id nvarchar(450), @grade int, @group int)
  as
  begin
	if((select count(UserId) from Students where UserId = @id) = 0)
		begin
			insert into Students values (@id, @grade, @group)
		end
	else
		begin
			if(@grade = 0 and @group != 0)
				begin
					update Students set [Group] = @group where [UserId] = @id
				end
			if(@group = 0 and @grade != 0)
				begin
					update Students set Grade = @grade where [UserId] = @id
				end
			if(@group = 0 and @grade = 0)
				begin
					return
				end
			if(@group != 0 and @grade != 0)
			begin
				update Students set [Grade] = @grade, [Group] = @group where [UserId] = @id
			end
		end
  end
            ";

            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var drop = "drop procedure InsertIntoStudents";
            migrationBuilder.Sql(drop);
        }
    }
}
