using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OptimizedEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessCode",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntityId",
                table: "Events",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "UpdateParameters",
                table: "Events",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateParameters",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntityId",
                table: "Events",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "BusinessCode",
                table: "Events",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
