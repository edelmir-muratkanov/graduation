using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_calculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calculations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    method_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calculations", x => x.id);
                    table.ForeignKey(
                        name: "fk_calculations_methods_method_id",
                        column: x => x.method_id,
                        principalTable: "methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_calculations_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "calculation_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calculation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_name = table.Column<string>(type: "text", nullable: false),
                    belonging = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calculation_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_calculation_items_calculations_calculation_id",
                        column: x => x.calculation_id,
                        principalTable: "calculations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_calculation_items_calculation_id",
                table: "calculation_items",
                column: "calculation_id");

            migrationBuilder.CreateIndex(
                name: "ix_calculation_items_property_name",
                table: "calculation_items",
                column: "property_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_calculations_method_id_project_id",
                table: "calculations",
                columns: new[] { "method_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "ix_calculations_project_id",
                table: "calculations",
                column: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculation_items");

            migrationBuilder.DropTable(
                name: "calculations");
        }
    }
}
