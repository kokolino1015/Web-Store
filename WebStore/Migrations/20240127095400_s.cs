using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cart_id",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    categoryid = table.Column<int>(name: "category_id", type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    cartid = table.Column<int>(name: "cart_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_cart_cart_id",
                        column: x => x.cartid,
                        principalTable: "cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_category_category_id",
                        column: x => x.categoryid,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_cart_id",
                table: "AspNetUsers",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_cart_id",
                table: "product",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_cart_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_cart_cart_id",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropIndex(
                name: "ix_asp_net_users_cart_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "cart_id",
                table: "AspNetUsers");
        }
    }
}
