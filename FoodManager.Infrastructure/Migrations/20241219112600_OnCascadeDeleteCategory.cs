using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnCascadeDeleteCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
