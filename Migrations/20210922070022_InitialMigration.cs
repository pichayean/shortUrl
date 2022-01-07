using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortUrl.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "short_url");

            migrationBuilder.CreateTable(
                name: "Url",
                schema: "short_url",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: false),
                    OriginalURL = table.Column<string>(type: "character(1024)", fixedLength: true, maxLength: 1024, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "(NOW())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Url",
                schema: "short_url");
        }
    }
}
