using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class u : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "cart_id",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "cart_id",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
