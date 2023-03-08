using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class StartPoint9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Check",
                table: "StoreRoomChats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Block",
                table: "Store",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Check",
                table: "RoomChats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Inventries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ReturnProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Reson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OderItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnProducts_OderItems_OderItemId",
                        column: x => x.OderItemId,
                        principalTable: "OderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProducts_OderItemId",
                table: "ReturnProducts",
                column: "OderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProducts_UserId",
                table: "ReturnProducts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnProducts");

            migrationBuilder.DropColumn(
                name: "Check",
                table: "StoreRoomChats");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Check",
                table: "RoomChats");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Inventries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
