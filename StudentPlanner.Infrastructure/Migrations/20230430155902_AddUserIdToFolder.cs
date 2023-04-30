using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.Infrastructure.Migrations
{
    public partial class AddUserIdToFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Folders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Folders");
        }
    }
}
