using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedPoint.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUserClaims_AspNetUsers_UserId",
                "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserLogins_AspNetUsers_UserId",
                "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserRoles_AspNetUsers_UserId",
                "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserTokens_AspNetUsers_UserId",
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "AspNetUsers");

            migrationBuilder.CreateTable(
                "ChannelStub",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ChannelStub", x => x.Id); });

            migrationBuilder.CreateTable(
                "GroupPermissions",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    IsSuperAdmin = table.Column<bool>(),
                    IsAdmin = table.Column<bool>(),
                    CanWrite = table.Column<bool>(),
                    CanView = table.Column<bool>(),
                    CanSendLinks = table.Column<bool>(),
                    CanAttachFiles = table.Column<bool>(),
                    CanManageServers = table.Column<bool>(),
                    CanManageChannels = table.Column<bool>()
                },
                constraints: table => { table.PrimaryKey("PK_GroupPermissions", x => x.Id); });

            migrationBuilder.CreateTable(
                "ServerStub",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ServerStub", x => x.Id); });

            migrationBuilder.CreateTable(
                "Groups",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ServerId = table.Column<int>(),
                    GroupPermissionsId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ChannelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        "FK_Groups_GroupPermissions_GroupPermissionsId",
                        x => x.GroupPermissionsId,
                        "GroupPermissions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Messages",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimePosted = table.Column<DateTime>(),
                    Text = table.Column<string>(nullable: true),
                    UserStubId = table.Column<int>(nullable: true),
                    ChannelStubId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ChannelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        "FK_Messages_ChannelStub_ChannelStubId",
                        x => x.ChannelStubId,
                        "ChannelStub",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Channels",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ChannelStubId = table.Column<int>(nullable: true),
                    ServerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        "FK_Channels_ChannelStub_ChannelStubId",
                        x => x.ChannelStubId,
                        "ChannelStub",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "UserStubs",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    AppUserName = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    ServerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStubs", x => x.Id);
                    table.ForeignKey(
                        "FK_UserStubs_Groups_GroupId",
                        x => x.GroupId,
                        "Groups",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<string>(),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(),
                    TwoFactorEnabled = table.Column<bool>(),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(),
                    AccessFailedCount = table.Column<int>(),
                    CurrentChannelId = table.Column<int>(),
                    CurrentServerIId = table.Column<int>(),
                    UserStubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_UserStubs_UserStubId",
                        x => x.UserStubId,
                        "UserStubs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Servers",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ServerStubId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        "FK_Servers_Users_ApplicationUserId",
                        x => x.ApplicationUserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Servers_ServerStub_ServerStubId",
                        x => x.ServerStubId,
                        "ServerStub",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Channels_ChannelStubId",
                "Channels",
                "ChannelStubId");

            migrationBuilder.CreateIndex(
                "IX_Channels_ServerId",
                "Channels",
                "ServerId");

            migrationBuilder.CreateIndex(
                "IX_Groups_ApplicationUserId",
                "Groups",
                "ApplicationUserId");

            migrationBuilder.CreateIndex(
                "IX_Groups_ChannelId",
                "Groups",
                "ChannelId");

            migrationBuilder.CreateIndex(
                "IX_Groups_GroupPermissionsId",
                "Groups",
                "GroupPermissionsId");

            migrationBuilder.CreateIndex(
                "IX_Groups_ServerId",
                "Groups",
                "ServerId");

            migrationBuilder.CreateIndex(
                "IX_Messages_ApplicationUserId",
                "Messages",
                "ApplicationUserId");

            migrationBuilder.CreateIndex(
                "IX_Messages_ChannelId",
                "Messages",
                "ChannelId");

            migrationBuilder.CreateIndex(
                "IX_Messages_ChannelStubId",
                "Messages",
                "ChannelStubId");

            migrationBuilder.CreateIndex(
                "IX_Messages_UserStubId",
                "Messages",
                "UserStubId");

            migrationBuilder.CreateIndex(
                "IX_Servers_ApplicationUserId",
                "Servers",
                "ApplicationUserId");

            migrationBuilder.CreateIndex(
                "IX_Servers_ServerStubId",
                "Servers",
                "ServerStubId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "Users",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "Users",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Users_UserStubId",
                "Users",
                "UserStubId");

            migrationBuilder.CreateIndex(
                "IX_UserStubs_GroupId",
                "UserStubs",
                "GroupId");

            migrationBuilder.CreateIndex(
                "IX_UserStubs_ServerId",
                "UserStubs",
                "ServerId");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserClaims_Users_UserId",
                "AspNetUserClaims",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserLogins_Users_UserId",
                "AspNetUserLogins",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserRoles_Users_UserId",
                "AspNetUserRoles",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserTokens_Users_UserId",
                "AspNetUserTokens",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Groups_Servers_ServerId",
                "Groups",
                "ServerId",
                "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Groups_Users_ApplicationUserId",
                "Groups",
                "ApplicationUserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Groups_Channels_ChannelId",
                "Groups",
                "ChannelId",
                "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Messages_Users_ApplicationUserId",
                "Messages",
                "ApplicationUserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Messages_Channels_ChannelId",
                "Messages",
                "ChannelId",
                "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Messages_UserStubs_UserStubId",
                "Messages",
                "UserStubId",
                "UserStubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Channels_Servers_ServerId",
                "Channels",
                "ServerId",
                "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_UserStubs_Servers_ServerId",
                "UserStubs",
                "ServerId",
                "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUserClaims_Users_UserId",
                "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserLogins_Users_UserId",
                "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserRoles_Users_UserId",
                "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                "FK_AspNetUserTokens_Users_UserId",
                "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                "FK_Channels_ChannelStub_ChannelStubId",
                "Channels");

            migrationBuilder.DropForeignKey(
                "FK_Channels_Servers_ServerId",
                "Channels");

            migrationBuilder.DropForeignKey(
                "FK_Groups_Servers_ServerId",
                "Groups");

            migrationBuilder.DropForeignKey(
                "FK_UserStubs_Servers_ServerId",
                "UserStubs");

            migrationBuilder.DropForeignKey(
                "FK_Groups_Users_ApplicationUserId",
                "Groups");

            migrationBuilder.DropTable(
                "Messages");

            migrationBuilder.DropTable(
                "ChannelStub");

            migrationBuilder.DropTable(
                "Servers");

            migrationBuilder.DropTable(
                "ServerStub");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "UserStubs");

            migrationBuilder.DropTable(
                "Groups");

            migrationBuilder.DropTable(
                "Channels");

            migrationBuilder.DropTable(
                "GroupPermissions");

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<string>(),
                    AccessFailedCount = table.Column<int>(),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(),
                    LockoutEnabled = table.Column<bool>(),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserClaims_AspNetUsers_UserId",
                "AspNetUserClaims",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserLogins_AspNetUsers_UserId",
                "AspNetUserLogins",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserRoles_AspNetUsers_UserId",
                "AspNetUserRoles",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_AspNetUserTokens_AspNetUsers_UserId",
                "AspNetUserTokens",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}