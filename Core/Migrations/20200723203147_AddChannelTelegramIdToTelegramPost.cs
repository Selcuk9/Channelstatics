using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddChannelTelegramIdToTelegramPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChannelTelegramId",
                table: "StatisticsPosts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelTelegramId",
                table: "StatisticsPosts");
        }
    }
}
