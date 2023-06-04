using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR_103_2019.Migrations
{
    /// <inheritdoc />
    public partial class poslednjeIzmeneBaze : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "User",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
