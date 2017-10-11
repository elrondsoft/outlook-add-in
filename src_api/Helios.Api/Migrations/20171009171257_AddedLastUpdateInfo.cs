using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Helios.Api.Migrations
{
    public partial class AddedLastUpdateInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeliosRefreshToken",
                table: "User",
                newName: "LastUpdateInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateInfo",
                table: "User",
                newName: "HeliosRefreshToken");
        }
    }
}
