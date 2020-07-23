using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticsChannels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    SubscribersCount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    ChannelUsername = table.Column<string>(nullable: true),
                    TelegramId = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChannels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramId = table.Column<long>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelUsername = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    WriteTime = table.Column<DateTime>(nullable: false),
                    EditTime = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPosts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticsChannels");

            migrationBuilder.DropTable(
                name: "StatisticsPosts");

            migrationBuilder.DropTable(
                name: "TelegramChannels");

            migrationBuilder.DropTable(
                name: "TelegramPosts");
        }
    }
}
