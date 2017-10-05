using Microsoft.EntityFrameworkCore.Migrations;

namespace Helios.Api.Migrations
{
    public partial class addapikeyproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "User");
        }
    }
}
