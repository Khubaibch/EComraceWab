using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Store_ReceiverId",
                table: "ChatsTable");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Users_ReceiverId",
                table: "ChatsTable",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Users_ReceiverId",
                table: "ChatsTable");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Store_ReceiverId",
                table: "ChatsTable",
                column: "ReceiverId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
