using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class qqqq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reviews_users_owner_id",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_reviews_owner_id",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "owner_id",
                table: "reviews",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_reviews_owner_id",
                table: "reviews",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_owner_id",
                table: "reviews",
                column: "owner_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");
        }
    }
}
