using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Alter_method_parameter_pk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_method_parameters",
                table: "method_parameters");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "method_parameters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_method_parameters",
                table: "method_parameters",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_method_parameters",
                table: "method_parameters");

            migrationBuilder.DropColumn(
                name: "id",
                table: "method_parameters");

            migrationBuilder.AddPrimaryKey(
                name: "pk_method_parameters",
                table: "method_parameters",
                columns: new[] { "method_id", "property_id" });
        }
    }
}
