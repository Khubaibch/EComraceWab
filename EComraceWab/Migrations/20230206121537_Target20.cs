using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsTable_Users_ModifiedById",
                table: "ChatsTable");

            migrationBuilder.DropIndex(
                name: "IX_ChatsTable_ModifiedById",
                table: "ChatsTable");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ChatsTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChatsTable");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_ModifiedById",
                table: "ChatsTable",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsTable_Users_ModifiedById",
                table: "ChatsTable",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
