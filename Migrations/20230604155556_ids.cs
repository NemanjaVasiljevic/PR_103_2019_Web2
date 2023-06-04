using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class ids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Article_ArticlesId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ArticlesId",
                table: "Order",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ArticlesId",
                table: "Order",
                newName: "IX_Order_ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Article_ArticleId",
                table: "Order",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Article_ArticleId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Order",
                newName: "ArticlesId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ArticleId",
                table: "Order",
                newName: "IX_Order_ArticlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Article_ArticlesId",
                table: "Order",
                column: "ArticlesId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
