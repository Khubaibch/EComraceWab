using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Targettarget211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "AssignToId",
                table: "Rooms",
                newName: "AssingToId");

            migrationBuilder.AlterColumn<int>(
                name: "AdmainId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AssingToId",
                table: "Rooms",
                column: "AssingToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms",
                column: "AdmainId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AssingToId",
                table: "Rooms",
                column: "AssingToId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AssingToId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AssingToId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "AssingToId",
                table: "Rooms",
                newName: "AssignToId");

            migrationBuilder.AlterColumn<int>(
                name: "AdmainId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms",
                column: "AdmainId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
