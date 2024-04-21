using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserAndRole : Migration
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "4ca3148a-cb03-4482-b56a-073929909a8f", "9a569935-99c4-4e1d-9724-74f2eebd5e83", "Employee", "EMPLOYEE" },
                    { "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "carts",
                column: "id",
                value: 1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3", 0, 1, "cbfa1742-4fde-4dfd-9de9-c7d0fe7a0c1e", "admin@webshop.com", true, null, null, false, null, "ADMIN@WEBSHOP.COM", "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEGIZOP1WMExjR6GCe6XwZwk5maMn0ao7EG8m584KvlCGOqzcfvuEL6zjzyd36HSJMA==", null, false, null, "7fb5fb4d-7f7b-4ebf-a64e-773d7ee86752", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3" });

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_carts_cart_id",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "4ca3148a-cb03-4482-b56a-073929909a8f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3");

            migrationBuilder.DeleteData(
                table: "carts",
                keyColumn: "id",
                keyValue: 1);

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
    }
}
