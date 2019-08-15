﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedPoint.Data;

namespace RedPoint.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181211015627_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RedPoint.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("CurrentChannelId");

                    b.Property<int>("CurrentServerIId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int?>("UserStubId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserStubId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChannelStubId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ServerId");

                    b.HasKey("Id");

                    b.HasIndex("ChannelStubId");

                    b.HasIndex("ServerId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.ChannelStub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ChannelStub");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("ChannelId");

                    b.Property<int?>("GroupPermissionsId");

                    b.Property<string>("Name");

                    b.Property<int>("ServerId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("GroupPermissionsId");

                    b.HasIndex("ServerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("ChannelId");

                    b.Property<int?>("ChannelStubId");

                    b.Property<DateTime>("DateTimePosted");

                    b.Property<string>("Text");

                    b.Property<int?>("UserStubId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ChannelStubId");

                    b.HasIndex("UserStubId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<int?>("ServerStubId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ServerStubId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.ServerStub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ServerStub");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.UserDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId");

                    b.Property<string>("AppUserName");

                    b.Property<int?>("GroupId");

                    b.Property<int?>("ServerId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ServerId");

                    b.ToTable("UserStubs");
                });

            modelBuilder.Entity("RedPoint.Models.Users_Permissions_Models.GroupPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanAttachFiles");

                    b.Property<bool>("CanManageChannels");

                    b.Property<bool>("CanManageServer");

                    b.Property<bool>("CanSendLinks");

                    b.Property<bool>("CanView");

                    b.Property<bool>("CanWrite");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsSuperAdmin");

                    b.HasKey("Id");

                    b.ToTable("GroupPermissions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RedPoint.Models.ApplicationUser", b =>
                {
                    b.HasOne("RedPoint.Models.Chat_Models.UserDTO", "UserDTO")
                        .WithMany()
                        .HasForeignKey("UserStubId");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Channel", b =>
                {
                    b.HasOne("RedPoint.Models.Chat_Models.ChannelStub", "ChannelStub")
                        .WithMany()
                        .HasForeignKey("ChannelStubId");

                    b.HasOne("RedPoint.Models.Chat_Models.Server")
                        .WithMany("Channels")
                        .HasForeignKey("ServerId");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Group", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany("Groups")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RedPoint.Models.Chat_Models.Channel")
                        .WithMany("Groups")
                        .HasForeignKey("ChannelId");

                    b.HasOne("RedPoint.Models.Users_Permissions_Models.GroupPermissions", "GroupPermissions")
                        .WithMany()
                        .HasForeignKey("GroupPermissionsId");

                    b.HasOne("RedPoint.Models.Chat_Models.Server")
                        .WithMany("Groups")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Message", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany("Messages")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RedPoint.Models.Chat_Models.Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId");

                    b.HasOne("RedPoint.Models.Chat_Models.ChannelStub", "ChannelStub")
                        .WithMany()
                        .HasForeignKey("ChannelStubId");

                    b.HasOne("RedPoint.Models.Chat_Models.UserDTO", "UserDTO")
                        .WithMany()
                        .HasForeignKey("UserStubId");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.Server", b =>
                {
                    b.HasOne("RedPoint.Models.ApplicationUser")
                        .WithMany("Servers")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RedPoint.Models.Chat_Models.ServerStub", "ServerStub")
                        .WithMany()
                        .HasForeignKey("ServerStubId");
                });

            modelBuilder.Entity("RedPoint.Models.Chat_Models.UserDTO", b =>
                {
                    b.HasOne("RedPoint.Models.Chat_Models.Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");

                    b.HasOne("RedPoint.Models.Chat_Models.Server")
                        .WithMany("Users")
                        .HasForeignKey("ServerId");
                });
#pragma warning restore 612, 618
        }
    }
}
