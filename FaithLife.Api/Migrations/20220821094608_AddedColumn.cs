using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FaithLife.Api.Migrations
{
    public partial class AddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    DayOfBirth = table.Column<int>(nullable: true),
                    MonthOfBirth = table.Column<int>(nullable: true),
                    YearOfBirth = table.Column<int>(nullable: true),
                    DayOfWedding = table.Column<int>(nullable: true),
                    MaritalStatus = table.Column<int>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    WorkForce = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
