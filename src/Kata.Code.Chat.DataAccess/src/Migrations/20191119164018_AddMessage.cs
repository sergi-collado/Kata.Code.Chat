using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kata.Code.Chat.DataAccess.Migrations
{
    public partial class AddMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    dateTime = table.Column<DateTime>(type: "date", nullable: false),
                    user = table.Column<string>(maxLength: 40, nullable: false),
                    message = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Message_PK", x => x.dateTime);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
