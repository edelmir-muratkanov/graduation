using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_calculation_item_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_calculation_items_calculation_id",
                table: "calculation_items");

            migrationBuilder.DropIndex(
                name: "ix_calculation_items_property_name",
                table: "calculation_items");

            migrationBuilder.CreateIndex(
                name: "ix_calculation_items_calculation_id_property_name",
                table: "calculation_items",
                columns: new[] { "calculation_id", "property_name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_calculation_items_calculation_id_property_name",
                table: "calculation_items");

            migrationBuilder.CreateIndex(
                name: "ix_calculation_items_calculation_id",
                table: "calculation_items",
                column: "calculation_id");

            migrationBuilder.CreateIndex(
                name: "ix_calculation_items_property_name",
                table: "calculation_items",
                column: "property_name",
                unique: true);
        }
    }
}
