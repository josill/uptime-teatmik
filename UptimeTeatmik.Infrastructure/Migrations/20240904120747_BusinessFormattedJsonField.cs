using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BusinessFormattedJsonField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormattedJson",
                table: "Businesses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormattedJson",
                table: "Businesses");
        }
    }
}
