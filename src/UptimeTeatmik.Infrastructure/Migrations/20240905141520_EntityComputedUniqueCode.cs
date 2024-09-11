using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityComputedUniqueCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                table: "Entities",
                type: "character varying(448)",
                maxLength: 448,
                nullable: false,
                computedColumnSql: "COALESCE(\"FirstName\", '') || COALESCE(\"BusinessOrLastName\", '') || \"BusinessOrPersonalCode\"",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "Entities");
        }
    }
}
