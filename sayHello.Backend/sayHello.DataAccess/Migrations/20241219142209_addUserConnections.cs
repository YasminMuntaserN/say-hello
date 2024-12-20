using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sayHello.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUserConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConversationDetails",
                columns: table => new
                {
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnReadMessagesCount = table.Column<int>(type: "int", nullable: false),
                    LastMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastMessageStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastMessageTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginForReceiver = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatRoom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationDetails");

            migrationBuilder.DropTable(
                name: "UserConnections");
        }
    }
}
