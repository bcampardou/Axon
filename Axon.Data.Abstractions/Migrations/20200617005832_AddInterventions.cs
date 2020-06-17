using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddInterventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NetworkIntervention",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<string>(nullable: true),
                    DataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkIntervention_Networks_DataId",
                        column: x => x.DataId,
                        principalTable: "Networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NetworkIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIntervention",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<string>(nullable: true),
                    DataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectIntervention_Projects_DataId",
                        column: x => x.DataId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerIntervention",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<string>(nullable: true),
                    DataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerIntervention_Servers_DataId",
                        column: x => x.DataId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetworkIntervention_DataId",
                table: "NetworkIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkIntervention_InChargeUserId",
                table: "NetworkIntervention",
                column: "InChargeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIntervention_DataId",
                table: "ProjectIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIntervention_InChargeUserId",
                table: "ProjectIntervention",
                column: "InChargeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerIntervention_DataId",
                table: "ServerIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerIntervention_InChargeUserId",
                table: "ServerIntervention",
                column: "InChargeUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetworkIntervention");

            migrationBuilder.DropTable(
                name: "ProjectIntervention");

            migrationBuilder.DropTable(
                name: "ServerIntervention");
        }
    }
}
