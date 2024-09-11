using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGeneratedUniqueCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueCode",
                table: "Entities",
                type: "character varying(448)",
                maxLength: 448,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(448)",
                oldMaxLength: 448,
                oldComputedColumnSql: "COALESCE(\"FirstName\", '') || COALESCE(\"BusinessOrLastName\", '') || \"BusinessOrPersonalCode\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueCode",
                table: "Entities",
                type: "character varying(448)",
                maxLength: 448,
                nullable: false,
                computedColumnSql: "COALESCE(\"FirstName\", '') || COALESCE(\"BusinessOrLastName\", '') || \"BusinessOrPersonalCode\"",
                stored: true,
                oldClrType: typeof(string),
                oldType: "character varying(448)",
                oldMaxLength: 448);
        }
    }
}
