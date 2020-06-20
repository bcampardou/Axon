using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Axon.Data.Abstractions.Migrations
{
    public partial class InitialPostgreSQLMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BusinessDocumentationUrl = table.Column<string>(nullable: true),
                    TechnicalDocumentationUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentationURL = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(nullable: true),
                    SecretKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnologies",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    TechnologyId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnologies", x => new { x.ProjectId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Key = table.Column<string>(nullable: true),
                    NumberOfAllowedUsers = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BusinessDocumentationUrl = table.Column<string>(nullable: true),
                    TechnicalDocumentationUrl = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Networks_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIntervention",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<Guid>(nullable: false),
                    DataId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectIntervention_Projects_DataId",
                        column: x => x.DataId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeammates",
                columns: table => new
                {
                    DataId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeammates", x => new { x.DataId, x.UserId });
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
                name: "NetworkIntervention",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<Guid>(nullable: false),
                    DataId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkIntervention_Networks_DataId",
                        column: x => x.DataId,
                        principalTable: "Networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetworkIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkTeammates",
                columns: table => new
                {
                    DataId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkTeammates", x => new { x.DataId, x.UserId });
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
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BusinessDocumentationUrl = table.Column<string>(nullable: true),
                    TechnicalDocumentationUrl = table.Column<string>(nullable: true),
                    OS = table.Column<int>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    NetworkId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Networks_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnvironments",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    URL = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnvironments", x => new { x.ProjectId, x.ServerId, x.Name });
                    table.ForeignKey(
                        name: "FK_ProjectEnvironments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEnvironments_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerIntervention",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()"),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    InChargeUserId = table.Column<Guid>(nullable: false),
                    DataId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerIntervention_Servers_DataId",
                        column: x => x.DataId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerIntervention_AspNetUsers_InChargeUserId",
                        column: x => x.InChargeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServersTeammates",
                columns: table => new
                {
                    DataId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    EditedAt = table.Column<DateTime>(nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServersTeammates", x => new { x.DataId, x.UserId });
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_TenantId",
                table: "AspNetRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_TenantId",
                table: "Licenses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkIntervention_DataId",
                table: "NetworkIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkIntervention_InChargeUserId",
                table: "NetworkIntervention",
                column: "InChargeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Networks_Name",
                table: "Networks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Networks_TenantId",
                table: "Networks",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkTeammates_UserId",
                table: "NetworkTeammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnvironments_Name",
                table: "ProjectEnvironments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnvironments_ServerId",
                table: "ProjectEnvironments",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIntervention_DataId",
                table: "ProjectIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIntervention_InChargeUserId",
                table: "ProjectIntervention",
                column: "InChargeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeammates_UserId",
                table: "ProjectTeammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnologies_TechnologyId",
                table: "ProjectTechnologies",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerIntervention_DataId",
                table: "ServerIntervention",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerIntervention_InChargeUserId",
                table: "ServerIntervention",
                column: "InChargeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_Name",
                table: "Servers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_NetworkId",
                table: "Servers",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ServersTeammates_UserId",
                table: "ServersTeammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_Name",
                table: "Technologies",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "NetworkIntervention");

            migrationBuilder.DropTable(
                name: "NetworkTeammates");

            migrationBuilder.DropTable(
                name: "ProjectEnvironments");

            migrationBuilder.DropTable(
                name: "ProjectIntervention");

            migrationBuilder.DropTable(
                name: "ProjectTeammates");

            migrationBuilder.DropTable(
                name: "ProjectTechnologies");

            migrationBuilder.DropTable(
                name: "ServerIntervention");

            migrationBuilder.DropTable(
                name: "ServersTeammates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");
        }
    }
}
