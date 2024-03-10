using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "c8e5ec43-5bc4-477d-b852-c5a7cd73ceed", "c8e5ec43-5bc4-477d-b852-c5a7cd73ceed", "Admin", "ADMIN" },
                    { "e0995bab-9f30-41b7-abc9-eccded60b9cc", "9c28cf23-ac4a-4199-984c-0f03368d3336", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "906701bc-fe8a-490c-9791-2f584af87223", 0, null, "0fa26696-3ef1-4cf5-ba45-07de889153ce", "admin@webshop.com", true, null, null, false, null, null, "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEDb6xSUdWhDDYSx/0DLjrgkwsTxvb7y8zxybU7CMET1mvij7WG42pP6MIV5eWGrQiw==", null, false, null, "b3e7da81-a63c-42b5-860e-5b71cb1ff2e7", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "c8e5ec43-5bc4-477d-b852-c5a7cd73ceed", "906701bc-fe8a-490c-9791-2f584af87223" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "e0995bab-9f30-41b7-abc9-eccded60b9cc");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "c8e5ec43-5bc4-477d-b852-c5a7cd73ceed", "906701bc-fe8a-490c-9791-2f584af87223" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c8e5ec43-5bc4-477d-b852-c5a7cd73ceed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "906701bc-fe8a-490c-9791-2f584af87223");
        }
    }
}
