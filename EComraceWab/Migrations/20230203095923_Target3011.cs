using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target3011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Users_CreatedById",
                table: "ChatsTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Users_ReceiverId",
                table: "ChatsTable");

            migrationBuilder.DropIndex(
                name: "IX_ChatsTable_CreatedById",
                table: "ChatsTable");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChatsTable");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Store_ReceiverId",
                table: "ChatsTable",
                column: "ReceiverId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Store_ReceiverId",
                table: "ChatsTable");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ChatsTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_CreatedById",
                table: "ChatsTable",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Users_CreatedById",
                table: "ChatsTable",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Users_ReceiverId",
                table: "ChatsTable",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
