using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OderItems_Colors_ColorsId",
                table: "OderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OderItems_Sizes_SizesId",
                table: "OderItems");

            migrationBuilder.DropTable(
                name: "ProductColorDetails");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_OderItems_ColorsId",
                table: "OderItems");

            migrationBuilder.DropColumn(
                name: "ColorsId",
                table: "OderItems");

            migrationBuilder.DropColumn(
                name: "ColorsId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SizesId",
                table: "OderItems",
                newName: "VariactionId");

            migrationBuilder.RenameIndex(
                name: "IX_OderItems_SizesId",
                table: "OderItems",
                newName: "IX_OderItems_VariactionId");

            migrationBuilder.RenameColumn(
                name: "SizesId",
                table: "Items",
                newName: "VariactionId");

            migrationBuilder.AddColumn<string>(
                name: "ColorVariaction",
                table: "ProductsVariactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_VariactionId",
                table: "Items",
                column: "VariactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ProductsVariactions_VariactionId",
                table: "Items",
                column: "VariactionId",
                principalTable: "ProductsVariactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OderItems_ProductsVariactions_VariactionId",
                table: "OderItems",
                column: "VariactionId",
                principalTable: "ProductsVariactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ProductsVariactions_VariactionId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_OderItems_ProductsVariactions_VariactionId",
                table: "OderItems");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_VariactionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ColorVariaction",
                table: "ProductsVariactions");

            migrationBuilder.RenameColumn(
                name: "VariactionId",
                table: "OderItems",
                newName: "SizesId");

            migrationBuilder.RenameIndex(
                name: "IX_OderItems_VariactionId",
                table: "OderItems",
                newName: "IX_OderItems_SizesId");

            migrationBuilder.RenameColumn(
                name: "VariactionId",
                table: "Items",
                newName: "SizesId");

            migrationBuilder.AddColumn<int>(
                name: "ColorsId",
                table: "OderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorsId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductColorDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorsId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColorDetails_Colors_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColorDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OderItems_ColorsId",
                table: "OderItems",
                column: "ColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorDetails_ColorsId",
                table: "ProductColorDetails",
                column: "ColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorDetails_ProductId",
                table: "ProductColorDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OderItems_Colors_ColorsId",
                table: "OderItems",
                column: "ColorsId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OderItems_Sizes_SizesId",
                table: "OderItems",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
