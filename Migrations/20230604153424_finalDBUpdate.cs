using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class finalDBUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Order_OrderId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_OrderId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Article");

            migrationBuilder.AddColumn<long>(
                name: "ArticlesId",
                table: "Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ArticlesId",
                table: "Order",
                column: "ArticlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Article_ArticlesId",
                table: "Order",
                column: "ArticlesId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Article_ArticlesId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ArticlesId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ArticlesId",
                table: "Order");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Article",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_OrderId",
                table: "Article",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Order_OrderId",
                table: "Article",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
