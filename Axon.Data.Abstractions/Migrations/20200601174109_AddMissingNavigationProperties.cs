using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddMissingNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NetworkId",
                table: "Server",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Server_NetworkId",
                table: "Server",
                column: "NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Network_NetworkId",
                table: "Server",
                column: "NetworkId",
                principalTable: "Network",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Server_Network_NetworkId",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Server_NetworkId",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Server");
        }
    }
}
