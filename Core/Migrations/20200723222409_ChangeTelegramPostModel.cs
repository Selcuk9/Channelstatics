using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ChangeTelegramPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChannelTelegramId",
                table: "TelegramPosts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "TelegramPosts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelTelegramId",
                table: "TelegramPosts");

            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "TelegramPosts");
        }
    }
}
