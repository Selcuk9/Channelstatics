using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class RemoveColummUserNameFromStatisticsPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "StatisticsPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "StatisticsPosts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
