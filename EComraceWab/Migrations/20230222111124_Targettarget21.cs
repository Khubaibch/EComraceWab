using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Targettarget21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_ReceiverId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ReceiverId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "AdmainId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignToId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "ChatsTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AdmainId",
                table: "Rooms",
                column: "AdmainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms",
                column: "AdmainId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AdmainId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AdmainId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AssignToId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "ChatsTable");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ReceiverId",
                table: "Rooms",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_ReceiverId",
                table: "Rooms",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
