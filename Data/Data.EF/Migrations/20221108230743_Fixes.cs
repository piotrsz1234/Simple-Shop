using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.EF.Migrations
{
    public partial class Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Product_FK_SaleProduct_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Sale_FK_SaleProduct_SaleId",
                table: "SaleProduct");

            migrationBuilder.DropIndex(
                name: "IX_SaleProduct_FK_SaleProduct_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropIndex(
                name: "IX_SaleProduct_FK_SaleProduct_SaleId",
                table: "SaleProduct");

            migrationBuilder.DropColumn(
                name: "FK_SaleProduct_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropColumn(
                name: "FK_SaleProduct_SaleId",
                table: "SaleProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_ProductId",
                table: "SaleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_SaleId",
                table: "SaleProduct",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Product_ProductId",
                table: "SaleProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Sale_SaleId",
                table: "SaleProduct",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Product_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Sale_SaleId",
                table: "SaleProduct");

            migrationBuilder.DropIndex(
                name: "IX_SaleProduct_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropIndex(
                name: "IX_SaleProduct_SaleId",
                table: "SaleProduct");

            migrationBuilder.AddColumn<long>(
                name: "FK_SaleProduct_ProductId",
                table: "SaleProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FK_SaleProduct_SaleId",
                table: "SaleProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_FK_SaleProduct_ProductId",
                table: "SaleProduct",
                column: "FK_SaleProduct_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_FK_SaleProduct_SaleId",
                table: "SaleProduct",
                column: "FK_SaleProduct_SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Product_FK_SaleProduct_ProductId",
                table: "SaleProduct",
                column: "FK_SaleProduct_ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Sale_FK_SaleProduct_SaleId",
                table: "SaleProduct",
                column: "FK_SaleProduct_SaleId",
                principalTable: "Sale",
                principalColumn: "Id");
        }
    }
}
