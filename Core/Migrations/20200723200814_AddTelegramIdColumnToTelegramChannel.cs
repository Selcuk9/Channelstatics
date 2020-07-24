using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddTelegramIdColumnToTelegramChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "StatisticsChannels",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "StatisticsChannels");
        }
    }
}
