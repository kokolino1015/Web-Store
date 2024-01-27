using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_cart_cart_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_role_role_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_product_cart_cart_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_product_category_category_id",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "pk_category",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart",
                table: "cart");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "carts");

            migrationBuilder.RenameIndex(
                name: "ix_product_category_id",
                table: "products",
                newName: "ix_products_category_id");

            migrationBuilder.RenameIndex(
                name: "ix_product_cart_id",
                table: "products",
                newName: "ix_products_cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_products",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_carts",
                table: "carts",
                column: "id");

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    ownerid = table.Column<string>(name: "owner_id", type: "text", nullable: true),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_owner_id",
                        column: x => x.ownerid,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reviews_owner_id",
                table: "reviews",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_roles_role_id",
                table: "AspNetUsers",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_carts_cart_id",
                table: "products",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_roles_role_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_products_carts_cart_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropPrimaryKey(
                name: "pk_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_carts",
                table: "carts");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "product");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "category");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "cart");

            migrationBuilder.RenameIndex(
                name: "ix_products_category_id",
                table: "product",
                newName: "ix_product_category_id");

            migrationBuilder.RenameIndex(
                name: "ix_products_cart_id",
                table: "product",
                newName: "ix_product_cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role",
                table: "role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product",
                table: "product",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_category",
                table: "category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart",
                table: "cart",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_cart_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_role_role_id",
                table: "AspNetUsers",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_cart_cart_id",
                table: "product",
                column: "cart_id",
                principalTable: "cart",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
