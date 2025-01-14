﻿// <auto-generated />
using System;
using Chat_App_Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chat_App_Database.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    partial class ChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chat_App_Library.Models.GeneralChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ChatBanned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxAmountPersons")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Private")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("GeneralChatDatabase");
                });

            modelBuilder.Entity("Chat_App_Library.Models.GroupChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ChatBanned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GroupOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("HashBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAmountPersons")
                        .HasColumnType("int");

                    b.Property<bool>("Private")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupOwnerId");

                    b.ToTable("GroupChatDatabase");
                });

            modelBuilder.Entity("Chat_App_Library.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("Grouptype")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("Chat_App_Library.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeneralChatId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupChatId")
                        .HasColumnType("int");

                    b.Property<int?>("SingleUserChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GeneralChatId");

                    b.HasIndex("GroupChatId");

                    b.HasIndex("SingleUserChatId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageDatabase");
                });

            modelBuilder.Entity("Chat_App_Library.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("jwtId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Chat_App_Library.Models.SingleUserChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ChatBanned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HashBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAmountPersons")
                        .HasColumnType("int");

                    b.Property<int?>("OriginUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Private")
                        .HasColumnType("bit");

                    b.Property<int?>("RecipientUserId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OriginUserId");

                    b.HasIndex("RecipientUserId");

                    b.ToTable("SingleUserChatDatabase");
                });

            modelBuilder.Entity("Chat_App_Library.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Banned")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GeneralChatId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupChatId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupChatId1")
                        .HasColumnType("int");

                    b.Property<string>("HashBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SingleUserChatId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralChatId");

                    b.HasIndex("GroupChatId");

                    b.HasIndex("GroupChatId1");

                    b.HasIndex("SingleUserChatId");

                    b.ToTable("UserDatabase");
                });

            modelBuilder.Entity("Chat_App_Library.Models.GeneralChat", b =>
                {
                    b.HasOne("Chat_App_Library.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Chat_App_Library.Models.GroupChat", b =>
                {
                    b.HasOne("Chat_App_Library.Models.User", "GroupOwner")
                        .WithMany()
                        .HasForeignKey("GroupOwnerId");

                    b.Navigation("GroupOwner");
                });

            modelBuilder.Entity("Chat_App_Library.Models.Invitation", b =>
                {
                    b.HasOne("Chat_App_Library.Models.User", null)
                        .WithMany("Invitations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Chat_App_Library.Models.Message", b =>
                {
                    b.HasOne("Chat_App_Library.Models.GeneralChat", null)
                        .WithMany("Messages")
                        .HasForeignKey("GeneralChatId");

                    b.HasOne("Chat_App_Library.Models.GroupChat", null)
                        .WithMany("Messages")
                        .HasForeignKey("GroupChatId");

                    b.HasOne("Chat_App_Library.Models.SingleUserChat", null)
                        .WithMany("Messages")
                        .HasForeignKey("SingleUserChatId");

                    b.HasOne("Chat_App_Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat_App_Library.Models.RefreshToken", b =>
                {
                    b.HasOne("Chat_App_Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chat_App_Library.Models.SingleUserChat", b =>
                {
                    b.HasOne("Chat_App_Library.Models.User", "OriginUser")
                        .WithMany()
                        .HasForeignKey("OriginUserId");

                    b.HasOne("Chat_App_Library.Models.User", "RecipientUser")
                        .WithMany()
                        .HasForeignKey("RecipientUserId");

                    b.Navigation("OriginUser");

                    b.Navigation("RecipientUser");
                });

            modelBuilder.Entity("Chat_App_Library.Models.User", b =>
                {
                    b.HasOne("Chat_App_Library.Models.GeneralChat", null)
                        .WithMany("BannedUsers")
                        .HasForeignKey("GeneralChatId");

                    b.HasOne("Chat_App_Library.Models.GroupChat", null)
                        .WithMany("BannedUsers")
                        .HasForeignKey("GroupChatId");

                    b.HasOne("Chat_App_Library.Models.GroupChat", null)
                        .WithMany("Users")
                        .HasForeignKey("GroupChatId1");

                    b.HasOne("Chat_App_Library.Models.SingleUserChat", null)
                        .WithMany("BannedUsers")
                        .HasForeignKey("SingleUserChatId");
                });

            modelBuilder.Entity("Chat_App_Library.Models.GeneralChat", b =>
                {
                    b.Navigation("BannedUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Chat_App_Library.Models.GroupChat", b =>
                {
                    b.Navigation("BannedUsers");

                    b.Navigation("Messages");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Chat_App_Library.Models.SingleUserChat", b =>
                {
                    b.Navigation("BannedUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Chat_App_Library.Models.User", b =>
                {
                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}
