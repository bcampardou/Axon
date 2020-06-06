using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddUserIsActiveAndNetworkTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Network",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Network_TenantId",
                table: "Network",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_TenantId",
                table: "AspNetRoles",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Tenant_TenantId",
                table: "AspNetRoles",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Network_Tenant_TenantId",
                table: "Network",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


            var tenantId = Guid.NewGuid().ToString();

            migrationBuilder.InsertData("Tenant", new string[] { "Id", "Name" },
            new object[] { tenantId, "AXON" });

            migrationBuilder.InsertData("License", new string[] { "Id", "Key", "StartDate", "EndDate", "IsActive", "TenantId" },
            new object[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now, DateTime.MaxValue, true, tenantId });

            migrationBuilder.InsertData("AspNetRoles", new string[] { "Id", "Name", "NormalizedName", "TenantId" },
                new object[] { Guid.NewGuid().ToString(), "Administrator", "ADMINISTRATOR", tenantId });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Tenant_TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Network_Tenant_TenantId",
                table: "Network");

            migrationBuilder.DropIndex(
                name: "IX_Network_TenantId",
                table: "Network");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Network");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetRoles");
        }
    }
}
