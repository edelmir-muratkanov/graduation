using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class alter_project_type_collector_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE public.projects ALTER COLUMN project_type TYPE smallint USING project_type::smallint");
            migrationBuilder.Sql(
                "ALTER TABLE public.projects ALTER COLUMN  collector_type TYPE smallint USING project_type::smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "project_type",
                table: "projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "collector_type",
                table: "projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
