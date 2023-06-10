using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class multipleArticleInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Article_ArticleId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ArticleId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Order_ArticleId",
                table: "Order",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Article_ArticleId",
                table: "Order",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
