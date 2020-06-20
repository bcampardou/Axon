using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class FillDefaultTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tenantId = Guid.NewGuid().ToString();

            migrationBuilder.InsertData("Tenants", new string[] { "Id", "Name" },
            new object[] { tenantId, "AXON" });

            migrationBuilder.InsertData("Licenses", new string[] { "Id", "Key", "StartDate", "EndDate", "IsActive", "TenantId", "NumberOfAllowedUsers" },
            new object[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now, DateTime.MaxValue, true, tenantId, Int32.MaxValue });

            migrationBuilder.InsertData("AspNetRoles", new string[] { "Id", "Name", "NormalizedName", "TenantId" },
                new object[] { Guid.NewGuid().ToString(), "Axon Admin", "AXON ADMIN", tenantId });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
