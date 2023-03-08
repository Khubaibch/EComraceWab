using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Start11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdmainId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AssingId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_SenderId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AdmainId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AssingId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_SenderId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AssingToId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "AssingId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssingId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssingToId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AdmainId",
                table: "Rooms",
                column: "AdmainId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AssingId",
                table: "Rooms",
                column: "AssingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_SenderId",
                table: "Rooms",
                column: "SenderId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Rooms_Users_AdmainId",
            //    table: "Rooms",
            //    column: "AdmainId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Rooms_Users_AssingId",
            //    table: "Rooms",
            //    column: "AssingId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Rooms_Users_SenderId",
            //    table: "Rooms",
            //    column: "SenderId",
            //    principalTable: "Users",
            //    principalColumn: "Id");
        }
    }
}
