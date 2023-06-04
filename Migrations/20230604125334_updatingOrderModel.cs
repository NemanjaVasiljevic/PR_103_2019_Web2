using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class updatingOrderModel : Migration
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

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "ArticleOrder",
                columns: table => new
                {
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    OrdersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOrder", x => new { x.ArticleId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_ArticleOrder_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleOrder_Order_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOrder_OrdersId",
                table: "ArticleOrder",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleOrder");

            migrationBuilder.AddColumn<long>(
                name: "ArticleId",
                table: "Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
