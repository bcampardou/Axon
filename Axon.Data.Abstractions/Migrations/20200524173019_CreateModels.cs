using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class CreateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    OS = table.Column<int>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentationURL = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnvironment",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    ServerId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    URL = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnvironment", x => new { x.ProjectId, x.ServerId, x.Name })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ProjectEnvironment_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEnvironment_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnology",
                columns: table => new
                {
                    ProjectId = table.Column<string>(nullable: false),
                    TechnologyId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnology", x => new { x.ProjectId, x.TechnologyId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Technology_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technology",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Network_Name",
                table: "Network",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnvironment_Name",
                table: "ProjectEnvironment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnvironment_ServerId",
                table: "ProjectEnvironment",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnology_TechnologyId",
                table: "ProjectTechnology",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Server_Name",
                table: "Server",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Technology_Name",
                table: "Technology",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Network");

            migrationBuilder.DropTable(
                name: "ProjectEnvironment");

            migrationBuilder.DropTable(
                name: "ProjectTechnology");

            migrationBuilder.DropTable(
                name: "Server");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technology");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");
        }
    }
}
