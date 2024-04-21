using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class PhotoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    fileextension = table.Column<string>(name: "file_extension", type: "text", nullable: false),
                    size = table.Column<decimal>(type: "numeric", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_photos_products_product_id",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "1e3718e2-769d-4c98-aedf-ab5f2dd7661a", "1e3718e2-769d-4c98-aedf-ab5f2dd7661a", "Admin", "ADMIN" },
                    { "c5fd0942-18bb-4913-870e-5587e324f3a9", "c67cf10c-138b-4efb-b93a-89f69f2684e4", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "cart_id", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "role_id", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "47c74ecd-7992-42e8-8312-692f049b2d3f", 0, 1, "4d77cdd0-7396-4ddb-ab16-39cc9a184751", "admin@webshop.com", true, null, null, false, null, "ADMIN@WEBSHOP.COM", "ADMIN@WEBSHOP.COM", "AQAAAAEAACcQAAAAEItSdUZtcgBF9bRYW/zsl9OgiNLfPN2eQiLV35XcFT53ZrpSvT36k+A9tFDsbVOg/Q==", null, false, null, "119cd12e-140d-415a-99bf-21a03a6a8872", false, "admin@webshop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "1e3718e2-769d-4c98-aedf-ab5f2dd7661a", "47c74ecd-7992-42e8-8312-692f049b2d3f" });

            migrationBuilder.CreateIndex(
                name: "ix_photos_product_id",
                table: "photos",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c5fd0942-18bb-4913-870e-5587e324f3a9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "1e3718e2-769d-4c98-aedf-ab5f2dd7661a", "47c74ecd-7992-42e8-8312-692f049b2d3f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "1e3718e2-769d-4c98-aedf-ab5f2dd7661a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "47c74ecd-7992-42e8-8312-692f049b2d3f");

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
    }
}
