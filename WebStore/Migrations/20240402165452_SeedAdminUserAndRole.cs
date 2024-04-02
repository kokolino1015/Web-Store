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
                    { "59c6cb87-d205-4ed3-98c3-cbe7d171afc0", "5b679292-4515-48b2-a035-af6e86c52345", "Employee", "EMPLOYEE" },
                    { "96311aef-454a-47f9-bc37-cd8665d740ca", "96311aef-454a-47f9-bc37-cd8665d740ca", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "carts",
                column: "id",
                value: 1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "63d64290-a4d8-4242-ac3e-2391f5a0b0aa", 0, 1, "72c5eb9b-cfd9-416c-ad4c-d83e012cff8f", "admin@webshop.com", true, null, null, false, null, "ADMIN@WEBSHOP.COM", "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEGlUnkT1rO/RVAalH8QrIlbpjvwBauDI+HLFWBdxmoPHOuOYIDUHaFQfT/XZ/GTZag==", null, false, null, "c0c671a1-0b9a-43f1-be33-fd237b520e8e", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "96311aef-454a-47f9-bc37-cd8665d740ca", "63d64290-a4d8-4242-ac3e-2391f5a0b0aa" });

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
                keyValue: "59c6cb87-d205-4ed3-98c3-cbe7d171afc0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "96311aef-454a-47f9-bc37-cd8665d740ca", "63d64290-a4d8-4242-ac3e-2391f5a0b0aa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "96311aef-454a-47f9-bc37-cd8665d740ca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "63d64290-a4d8-4242-ac3e-2391f5a0b0aa");

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
