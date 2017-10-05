using Microsoft.EntityFrameworkCore.Migrations;

namespace Helios.Api.Migrations
{
    public partial class hash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MicrosoftLogin",
                table: "User",
                newName: "TasksSyncHash");

            migrationBuilder.AddColumn<string>(
                name: "EventsSyncHash",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventsSyncHash",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "TasksSyncHash",
                table: "User",
                newName: "MicrosoftLogin");
        }
    }
}
