using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    HeliosLogin = table.Column<string>(nullable: true),
                    HeliosPassword = table.Column<string>(nullable: true),
                    HeliosRefreshToken = table.Column<string>(nullable: true),
                    HeliosToken = table.Column<string>(nullable: true),
                    IsSyncEnabled = table.Column<bool>(nullable: false),
                    MicrosoftLogin = table.Column<string>(nullable: true),
                    MicrosoftRefreshToken = table.Column<string>(nullable: true),
                    MicrosoftToken = table.Column<string>(nullable: true)
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
