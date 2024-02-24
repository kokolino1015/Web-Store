using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class qi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "amount",
                table: "payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cart_id",
                table: "payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_payments_cart_id",
                table: "payments",
                column: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payments_carts_cart_id",
                table: "payments",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payments_carts_cart_id",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payments_cart_id",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "cart_id",
                table: "payments");
        }
    }
}
