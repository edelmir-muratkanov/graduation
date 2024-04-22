using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_method : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    collector_types = table.Column<byte[]>(type: "smallint[]", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "method_parameters",
                columns: table => new
                {
                    method_id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_parameters = table.Column<string>(type: "jsonb", nullable: true),
                    second_parameters = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_method_parameters", x => new { x.method_id, x.property_id });
                    table.ForeignKey(
                        name: "fk_method_parameters_methods_method_id",
                        column: x => x.method_id,
                        principalTable: "methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_method_parameters_properties_property_id",
                        column: x => x.property_id,
                        principalTable: "properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_method_parameters_method_id_property_id",
                table: "method_parameters",
                columns: new[] { "method_id", "property_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_method_parameters_property_id",
                table: "method_parameters",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "ix_methods_name",
                table: "methods",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "method_parameters");

            migrationBuilder.DropTable(
                name: "methods");
        }
    }
}
