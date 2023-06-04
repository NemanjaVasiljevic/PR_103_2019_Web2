using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class articleOrderConn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleOrder");

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
    }
}
