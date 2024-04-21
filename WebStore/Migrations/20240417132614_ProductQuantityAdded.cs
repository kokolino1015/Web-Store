using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class ProductQuantityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "4711f7b2-ba05-4fbe-b6ee-80aa7eaeb362", "6c4a779a-49a6-482b-a03b-a5a73edea6bf", "Employee", "EMPLOYEE" },
                    { "5b14f8cc-6cc0-4d0c-bcdf-264f1e98a0a6", "5b14f8cc-6cc0-4d0c-bcdf-264f1e98a0a6", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "51a11227-9269-4140-a289-50f0918fcc98", 0, 1, "df6e885f-3984-4379-ac46-a7bdeb6a9746", "admin@webshop.com", true, null, null, false, null, "ADMIN@WEBSHOP.COM", "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEElGuXhy1KiUIp5zUv/SVk0knwmCW1k9j0NWP2SjSuDrxHHmXflmb6BaxfxJsPakYw==", null, false, null, "bf000291-d6a7-4a1b-84ee-859823fad396", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "5b14f8cc-6cc0-4d0c-bcdf-264f1e98a0a6", "51a11227-9269-4140-a289-50f0918fcc98" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "4711f7b2-ba05-4fbe-b6ee-80aa7eaeb362");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "5b14f8cc-6cc0-4d0c-bcdf-264f1e98a0a6", "51a11227-9269-4140-a289-50f0918fcc98" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "5b14f8cc-6cc0-4d0c-bcdf-264f1e98a0a6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "51a11227-9269-4140-a289-50f0918fcc98");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "4ca3148a-cb03-4482-b56a-073929909a8f", "9a569935-99c4-4e1d-9724-74f2eebd5e83", "Employee", "EMPLOYEE" },
                    { "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3", 0, 1, "cbfa1742-4fde-4dfd-9de9-c7d0fe7a0c1e", "admin@webshop.com", true, null, null, false, null, "ADMIN@WEBSHOP.COM", "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEGIZOP1WMExjR6GCe6XwZwk5maMn0ao7EG8m584KvlCGOqzcfvuEL6zjzyd36HSJMA==", null, false, null, "7fb5fb4d-7f7b-4ebf-a64e-773d7ee86752", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "c3cdb8ca-c56c-4e62-8ff7-8b77f071454e", "b0a482ae-b1a0-4584-abbd-bc9f27c76eb3" });
        }
    }
}
