using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_EntityId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_EntityId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Entities");

            migrationBuilder.CreateTable(
                name: "EntityOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleInEntityAbbreviation = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    RoleInEntity = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnedId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityOwners_Entities_OwnedId",
                        column: x => x.OwnedId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityOwners_Entities_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityOwners_OwnedId",
                table: "EntityOwners",
                column: "OwnedId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityOwners_OwnerId",
                table: "EntityOwners",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityOwners");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "Entities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_EntityId",
                table: "Entities",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_EntityId",
                table: "Entities",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id");
        }
    }
}
