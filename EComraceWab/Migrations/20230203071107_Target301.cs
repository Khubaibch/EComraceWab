using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target301 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatsTable_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatsTable_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatsTable_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatsTable_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_CreatedById",
                table: "ChatsTable",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_ModifiedById",
                table: "ChatsTable",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_ReceiverId",
                table: "ChatsTable",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsTable_SenderId",
                table: "ChatsTable",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatsTable");
        }
    }
}
