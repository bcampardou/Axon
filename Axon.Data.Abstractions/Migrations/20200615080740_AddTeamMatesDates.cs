using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddTeamMatesDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ServersTeammates",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "ServersTeammates",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectTeammates",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "ProjectTeammates",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "NetworkTeammates",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "NetworkTeammates",
                nullable: true,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ServersTeammates");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "ServersTeammates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectTeammates");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "ProjectTeammates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "NetworkTeammates");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "NetworkTeammates");
        }
    }
}
