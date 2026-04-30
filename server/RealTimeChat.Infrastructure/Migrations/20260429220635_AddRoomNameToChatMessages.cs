using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomNameToChatMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "ChatMessages");
        }
    }
}
