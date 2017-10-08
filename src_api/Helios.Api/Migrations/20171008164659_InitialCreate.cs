using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Helios.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApiKey = table.Column<string>(nullable: true),
                    EntityId = table.Column<string>(nullable: true),
                    EventsSyncHash = table.Column<string>(nullable: true),
                    HeliosLogin = table.Column<string>(nullable: true),
                    HeliosPassword = table.Column<string>(nullable: true),
                    HeliosRefreshToken = table.Column<string>(nullable: true),
                    HeliosToken = table.Column<string>(nullable: true),
                    IsSyncEnabled = table.Column<bool>(nullable: false),
                    MicrosoftRefreshToken = table.Column<string>(nullable: true),
                    MicrosoftToken = table.Column<string>(nullable: true),
                    TasksSyncHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
