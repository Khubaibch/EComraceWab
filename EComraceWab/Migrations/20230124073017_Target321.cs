using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComraceWab.Migrations
{
    /// <inheritdoc />
    public partial class Target321 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OderItems_Colores_ColorsId",
                table: "OderItems");

            migrationBuilder.DropTable(
                name: "ProductColoresDetail");

            migrationBuilder.DropTable(
                name: "ProductSizesDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colores",
                table: "Colores");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Colores",
                newName: "Colors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductsVariactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Variaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsVariactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsVariactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventries_ProductsVariactions_VariactionId",
                        column: x => x.VariactionId,
                        principalTable: "ProductsVariactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventries_VariactionId",
                table: "Inventries",
                column: "VariactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsVariactions_ProductId",
                table: "ProductsVariactions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OderItems_Colors_ColorsId",
                table: "OderItems",
                column: "ColorsId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OderItems_Colors_ColorsId",
                table: "OderItems");

            migrationBuilder.DropTable(
                name: "Inventries");

            migrationBuilder.DropTable(
                name: "ProductsVariactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Colores");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colores",
                table: "Colores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductColoresDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorsId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColoresDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColoresDetail_Colores_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColoresDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizesDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizesDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSizesDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColoresDetail_ColorsId",
                table: "ProductColoresDetail",
                column: "ColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColoresDetail_ProductId",
                table: "ProductColoresDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizesDetail_ProductId",
                table: "ProductSizesDetail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OderItems_Colores_ColorsId",
                table: "OderItems",
                column: "ColorsId",
                principalTable: "Colores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
