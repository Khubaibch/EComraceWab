using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Start1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomChats_Users_CreaterId",
                table: "RoomChats");

            migrationBuilder.DropIndex(
                name: "IX_RoomChats_CreaterId",
                table: "RoomChats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomChats_CreaterId",
                table: "RoomChats",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomChats_Users_CreaterId",
                table: "RoomChats",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
