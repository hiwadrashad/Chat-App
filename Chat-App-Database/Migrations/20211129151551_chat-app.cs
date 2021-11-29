using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat_App_Database.Migrations
{
    public partial class chatapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralChatId = table.Column<int>(type: "int", nullable: true),
                    GroupChatId = table.Column<int>(type: "int", nullable: true),
                    SingleUserChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false),
                    GeneralChatId = table.Column<int>(type: "int", nullable: true),
                    GroupChatId = table.Column<int>(type: "int", nullable: true),
                    GroupChatId1 = table.Column<int>(type: "int", nullable: true),
                    SingleUserChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralChatDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    ChatBanned = table.Column<bool>(type: "bit", nullable: false),
                    MaxAmountPersons = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralChatDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralChatDatabase_UserDatabase_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupOwnerId = table.Column<int>(type: "int", nullable: true),
                    ChatBanned = table.Column<bool>(type: "bit", nullable: false),
                    MaxAmountPersons = table.Column<int>(type: "int", nullable: false),
                    HashBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatDatabase_UserDatabase_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Grouptype = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_UserDatabase_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_UserDatabase_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleUserChatDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginUserId = table.Column<int>(type: "int", nullable: true),
                    RecipientUserId = table.Column<int>(type: "int", nullable: true),
                    ChatBanned = table.Column<bool>(type: "bit", nullable: false),
                    MaxAmountPersons = table.Column<int>(type: "int", nullable: false),
                    HashBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleUserChatDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleUserChatDatabase_UserDatabase_OriginUserId",
                        column: x => x.OriginUserId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleUserChatDatabase_UserDatabase_RecipientUserId",
                        column: x => x.RecipientUserId,
                        principalTable: "UserDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralChatDatabase_OwnerId",
                table: "GeneralChatDatabase",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatDatabase_GroupOwnerId",
                table: "GroupChatDatabase",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDatabase_GeneralChatId",
                table: "MessageDatabase",
                column: "GeneralChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDatabase_GroupChatId",
                table: "MessageDatabase",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDatabase_SingleUserChatId",
                table: "MessageDatabase",
                column: "SingleUserChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDatabase_UserId",
                table: "MessageDatabase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleUserChatDatabase_OriginUserId",
                table: "SingleUserChatDatabase",
                column: "OriginUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleUserChatDatabase_RecipientUserId",
                table: "SingleUserChatDatabase",
                column: "RecipientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatabase_GeneralChatId",
                table: "UserDatabase",
                column: "GeneralChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatabase_GroupChatId",
                table: "UserDatabase",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatabase_GroupChatId1",
                table: "UserDatabase",
                column: "GroupChatId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatabase_SingleUserChatId",
                table: "UserDatabase",
                column: "SingleUserChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDatabase_GeneralChatDatabase_GeneralChatId",
                table: "MessageDatabase",
                column: "GeneralChatId",
                principalTable: "GeneralChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDatabase_GroupChatDatabase_GroupChatId",
                table: "MessageDatabase",
                column: "GroupChatId",
                principalTable: "GroupChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDatabase_SingleUserChatDatabase_SingleUserChatId",
                table: "MessageDatabase",
                column: "SingleUserChatId",
                principalTable: "SingleUserChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDatabase_UserDatabase_UserId",
                table: "MessageDatabase",
                column: "UserId",
                principalTable: "UserDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatabase_GeneralChatDatabase_GeneralChatId",
                table: "UserDatabase",
                column: "GeneralChatId",
                principalTable: "GeneralChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatabase_GroupChatDatabase_GroupChatId",
                table: "UserDatabase",
                column: "GroupChatId",
                principalTable: "GroupChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatabase_GroupChatDatabase_GroupChatId1",
                table: "UserDatabase",
                column: "GroupChatId1",
                principalTable: "GroupChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatabase_SingleUserChatDatabase_SingleUserChatId",
                table: "UserDatabase",
                column: "SingleUserChatId",
                principalTable: "SingleUserChatDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralChatDatabase_UserDatabase_OwnerId",
                table: "GeneralChatDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatDatabase_UserDatabase_GroupOwnerId",
                table: "GroupChatDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleUserChatDatabase_UserDatabase_OriginUserId",
                table: "SingleUserChatDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleUserChatDatabase_UserDatabase_RecipientUserId",
                table: "SingleUserChatDatabase");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "MessageDatabase");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserDatabase");

            migrationBuilder.DropTable(
                name: "GeneralChatDatabase");

            migrationBuilder.DropTable(
                name: "GroupChatDatabase");

            migrationBuilder.DropTable(
                name: "SingleUserChatDatabase");
        }
    }
}
