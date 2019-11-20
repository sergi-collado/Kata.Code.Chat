using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kata.Code.Chat.DataAccess.Migrations
{
    public partial class ChangeDateTimeTypeAndMessageLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "Messages",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTime",
                table: "Messages",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "Messages",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateTime",
                table: "Messages",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
