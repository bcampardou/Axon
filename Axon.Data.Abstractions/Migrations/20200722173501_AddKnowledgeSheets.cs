using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddKnowledgeSheets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnowledgeSheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Title = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeSheet_KnowledgeSheet_ParentId",
                        column: x => x.ParentId,
                        principalTable: "KnowledgeSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeSheet_ParentId",
                table: "KnowledgeSheet",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeSheet");
        }
    }
}
