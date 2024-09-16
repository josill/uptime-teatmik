using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventTypesOnSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int[]>(
                name: "EventTypes",
                table: "Subscriptions",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTypes",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "Subscriptions",
                type: "integer",
                nullable: true);
        }
    }
}
