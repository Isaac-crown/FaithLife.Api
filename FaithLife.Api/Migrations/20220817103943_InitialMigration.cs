using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FaithLife.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(nullable: true),
                    EventDescription = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Services = table.Column<int>(nullable: false),
                    Minister = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Created", "EventDescription", "EventName", "Minister", "Services" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "God will send his Holy spirit to you", "The Holy spirit", "Pastor Abraham", 0 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Created", "EventDescription", "EventName", "Minister", "Services" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "God will send his Holy spirit to you", "The Holy spirit", "Pastor Abraham", 0 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Created", "EventDescription", "EventName", "Minister", "Services" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "God will send his Holy spirit to you", "The Holy spirit", "Pastor Abraham", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
