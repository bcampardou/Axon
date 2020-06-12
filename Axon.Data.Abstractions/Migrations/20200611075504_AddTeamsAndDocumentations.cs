using Microsoft.EntityFrameworkCore.Migrations;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class AddTeamsAndDocumentations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Tenant_TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_License_Tenant_TenantId",
                table: "License");

            migrationBuilder.DropForeignKey(
                name: "FK_Network_Tenant_TenantId",
                table: "Network");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnvironment_Projects_ProjectId",
                table: "ProjectEnvironment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnvironment_Server_ServerId",
                table: "ProjectEnvironment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnology_Projects_ProjectId",
                table: "ProjectTechnology");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnology_Technology_TechnologyId",
                table: "ProjectTechnology");

            migrationBuilder.DropForeignKey(
                name: "FK_Server_Network_NetworkId",
                table: "Server");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technology",
                table: "Technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Server",
                table: "Server");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTechnology",
                table: "ProjectTechnology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEnvironment",
                table: "ProjectEnvironment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Network",
                table: "Network");

            migrationBuilder.DropPrimaryKey(
                name: "PK_License",
                table: "License");

            migrationBuilder.RenameTable(
                name: "Tenant",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "Technology",
                newName: "Technologies");

            migrationBuilder.RenameTable(
                name: "Server",
                newName: "Servers");

            migrationBuilder.RenameTable(
                name: "ProjectTechnology",
                newName: "ProjectTechnologies");

            migrationBuilder.RenameTable(
                name: "ProjectEnvironment",
                newName: "ProjectEnvironments");

            migrationBuilder.RenameTable(
                name: "Network",
                newName: "Networks");

            migrationBuilder.RenameTable(
                name: "License",
                newName: "Licenses");

            migrationBuilder.RenameIndex(
                name: "IX_Technology_Name",
                table: "Technologies",
                newName: "IX_Technologies_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Server_NetworkId",
                table: "Servers",
                newName: "IX_Servers_NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_Server_Name",
                table: "Servers",
                newName: "IX_Servers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTechnology_TechnologyId",
                table: "ProjectTechnologies",
                newName: "IX_ProjectTechnologies_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEnvironment_ServerId",
                table: "ProjectEnvironments",
                newName: "IX_ProjectEnvironments_ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEnvironment_Name",
                table: "ProjectEnvironments",
                newName: "IX_ProjectEnvironments_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Network_TenantId",
                table: "Networks",
                newName: "IX_Networks_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Network_Name",
                table: "Networks",
                newName: "IX_Networks_Name");

            migrationBuilder.RenameIndex(
                name: "IX_License_TenantId",
                table: "Licenses",
                newName: "IX_Licenses_TenantId");

            migrationBuilder.AddColumn<string>(
                name: "BusinessDocumentationUrl",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDocumentationUrl",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessDocumentationUrl",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDocumentationUrl",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessDocumentationUrl",
                table: "Networks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalDocumentationUrl",
                table: "Networks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAllowedUsers",
                table: "Licenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servers",
                table: "Servers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTechnologies",
                table: "ProjectTechnologies",
                columns: new[] { "ProjectId", "TechnologyId" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEnvironments",
                table: "ProjectEnvironments",
                columns: new[] { "ProjectId", "ServerId", "Name" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Networks",
                table: "Networks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Licenses",
                table: "Licenses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NetworkTeammates",
                columns: table => new
                {
                    DataId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkTeammates", x => new { x.DataId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_NetworkTeammates_Networks_DataId",
                        column: x => x.DataId,
                        principalTable: "Networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetworkTeammates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeammates",
                columns: table => new
                {
                    DataId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeammates", x => new { x.DataId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ProjectTeammates_Projects_DataId",
                        column: x => x.DataId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeammates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServersTeammates",
                columns: table => new
                {
                    DataId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServersTeammates", x => new { x.DataId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ServersTeammates_Servers_DataId",
                        column: x => x.DataId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServersTeammates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetworkTeammates_UserId",
                table: "NetworkTeammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeammates_UserId",
                table: "ProjectTeammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServersTeammates_UserId",
                table: "ServersTeammates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Tenants_TenantId",
                table: "AspNetRoles",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenants_TenantId",
                table: "AspNetUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Tenants_TenantId",
                table: "Licenses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Networks_Tenants_TenantId",
                table: "Networks",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnvironments_Projects_ProjectId",
                table: "ProjectEnvironments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnvironments_Servers_ServerId",
                table: "ProjectEnvironments",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnologies_Projects_ProjectId",
                table: "ProjectTechnologies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnologies_Technologies_TechnologyId",
                table: "ProjectTechnologies",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Networks_NetworkId",
                table: "Servers",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Tenants_TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenants_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Tenants_TenantId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Networks_Tenants_TenantId",
                table: "Networks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnvironments_Projects_ProjectId",
                table: "ProjectEnvironments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnvironments_Servers_ServerId",
                table: "ProjectEnvironments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnologies_Projects_ProjectId",
                table: "ProjectTechnologies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTechnologies_Technologies_TechnologyId",
                table: "ProjectTechnologies");

            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Networks_NetworkId",
                table: "Servers");

            migrationBuilder.DropTable(
                name: "NetworkTeammates");

            migrationBuilder.DropTable(
                name: "ProjectTeammates");

            migrationBuilder.DropTable(
                name: "ServersTeammates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servers",
                table: "Servers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTechnologies",
                table: "ProjectTechnologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEnvironments",
                table: "ProjectEnvironments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Networks",
                table: "Networks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Licenses",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "BusinessDocumentationUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TechnicalDocumentationUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BusinessDocumentationUrl",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "TechnicalDocumentationUrl",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "BusinessDocumentationUrl",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "TechnicalDocumentationUrl",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "NumberOfAllowedUsers",
                table: "Licenses");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Tenant");

            migrationBuilder.RenameTable(
                name: "Technologies",
                newName: "Technology");

            migrationBuilder.RenameTable(
                name: "Servers",
                newName: "Server");

            migrationBuilder.RenameTable(
                name: "ProjectTechnologies",
                newName: "ProjectTechnology");

            migrationBuilder.RenameTable(
                name: "ProjectEnvironments",
                newName: "ProjectEnvironment");

            migrationBuilder.RenameTable(
                name: "Networks",
                newName: "Network");

            migrationBuilder.RenameTable(
                name: "Licenses",
                newName: "License");

            migrationBuilder.RenameIndex(
                name: "IX_Technologies_Name",
                table: "Technology",
                newName: "IX_Technology_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_NetworkId",
                table: "Server",
                newName: "IX_Server_NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_Name",
                table: "Server",
                newName: "IX_Server_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTechnologies_TechnologyId",
                table: "ProjectTechnology",
                newName: "IX_ProjectTechnology_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEnvironments_ServerId",
                table: "ProjectEnvironment",
                newName: "IX_ProjectEnvironment_ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEnvironments_Name",
                table: "ProjectEnvironment",
                newName: "IX_ProjectEnvironment_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Networks_TenantId",
                table: "Network",
                newName: "IX_Network_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Networks_Name",
                table: "Network",
                newName: "IX_Network_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Licenses_TenantId",
                table: "License",
                newName: "IX_License_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technology",
                table: "Technology",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Server",
                table: "Server",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTechnology",
                table: "ProjectTechnology",
                columns: new[] { "ProjectId", "TechnologyId" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEnvironment",
                table: "ProjectEnvironment",
                columns: new[] { "ProjectId", "ServerId", "Name" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Network",
                table: "Network",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_License",
                table: "License",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Tenant_TenantId",
                table: "AspNetRoles",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_License_Tenant_TenantId",
                table: "License",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnvironment_Projects_ProjectId",
                table: "ProjectEnvironment",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnvironment_Server_ServerId",
                table: "ProjectEnvironment",
                column: "ServerId",
                principalTable: "Server",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnology_Projects_ProjectId",
                table: "ProjectTechnology",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTechnology_Technology_TechnologyId",
                table: "ProjectTechnology",
                column: "TechnologyId",
                principalTable: "Technology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Network_NetworkId",
                table: "Server",
                column: "NetworkId",
                principalTable: "Network",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
