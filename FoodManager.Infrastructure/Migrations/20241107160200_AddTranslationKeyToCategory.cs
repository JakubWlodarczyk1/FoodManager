using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTranslationKeyToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranslationKey",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslationKey",
                table: "Categories");
        }
    }
}
