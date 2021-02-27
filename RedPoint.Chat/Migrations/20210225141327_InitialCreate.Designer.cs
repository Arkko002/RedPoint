﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedPoint.Chat.Data;

namespace RedPoint.Chat.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20210225141327_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ChatUserGroup", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<string>("UsersId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ChatUserGroup");
                });

            modelBuilder.Entity("ChatUserServer", b =>
                {
                    b.Property<int>("ServersId")
                        .HasColumnType("int");

                    b.Property<string>("UsersId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("ServersId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ChatUserServer");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupId")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.ChatUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentChannelId")
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentServerId")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserSettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ChannelId")
                        .HasColumnType("int");

                    b.Property<int>("GroupPermissions")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ServerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimePosted")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupId")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.User_Settings.ChatSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ChatSettings");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.User_Settings.PrivacySettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("CanBeSearched")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("PrivacySettings");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.User_Settings.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ChatSettingsId")
                        .HasColumnType("int");

                    b.Property<int?>("PrivacySettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatSettingsId");

                    b.HasIndex("PrivacySettingsId");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("ChatUserGroup", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedPoint.Chat.Models.Chat.ChatUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChatUserServer", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.Server", null)
                        .WithMany()
                        .HasForeignKey("ServersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedPoint.Chat.Models.Chat.ChatUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Channel", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.Server", null)
                        .WithMany("Channels")
                        .HasForeignKey("ServerId");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.ChatUser", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.User_Settings.UserSettings", "UserSettings")
                        .WithMany()
                        .HasForeignKey("UserSettingsId");

                    b.Navigation("UserSettings");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Group", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.Channel", null)
                        .WithMany("Groups")
                        .HasForeignKey("ChannelId");

                    b.HasOne("RedPoint.Chat.Models.Chat.Server", "Server")
                        .WithMany("Groups")
                        .HasForeignKey("ServerId");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Message", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.Channel", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId");

                    b.HasOne("RedPoint.Chat.Models.Chat.ChatUser", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.User_Settings.UserSettings", b =>
                {
                    b.HasOne("RedPoint.Chat.Models.Chat.User_Settings.ChatSettings", "ChatSettings")
                        .WithMany()
                        .HasForeignKey("ChatSettingsId");

                    b.HasOne("RedPoint.Chat.Models.Chat.User_Settings.PrivacySettings", "PrivacySettings")
                        .WithMany()
                        .HasForeignKey("PrivacySettingsId");

                    b.Navigation("ChatSettings");

                    b.Navigation("PrivacySettings");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Channel", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.ChatUser", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("RedPoint.Chat.Models.Chat.Server", b =>
                {
                    b.Navigation("Channels");

                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
