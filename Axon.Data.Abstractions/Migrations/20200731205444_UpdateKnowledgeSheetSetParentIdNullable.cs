using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class UpdateKnowledgeSheetSetParentIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeSheet_KnowledgeSheet_ParentId",
                table: "KnowledgeSheet");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "KnowledgeSheet",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeSheet_KnowledgeSheet_ParentId",
                table: "KnowledgeSheet",
                column: "ParentId",
                principalTable: "KnowledgeSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeSheet_KnowledgeSheet_ParentId",
                table: "KnowledgeSheet");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "KnowledgeSheet",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeSheet_KnowledgeSheet_ParentId",
                table: "KnowledgeSheet",
                column: "ParentId",
                principalTable: "KnowledgeSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
