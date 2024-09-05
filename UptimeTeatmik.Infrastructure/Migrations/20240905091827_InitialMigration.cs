using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessRegisterEntities",
                columns: table => new
                {
                    BusinessRegisterEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessOrPersonalCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    BusinessOrLastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    BusinessRegisterEntityTypeAbbreviation = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    BusinessRegisterEntityType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FormattedJson = table.Column<string>(type: "text", nullable: true),
                    BusinessRegisterEntityId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRegisterEntities", x => x.BusinessRegisterEntityId);
                    table.ForeignKey(
                        name: "FK_BusinessRegisterEntities_BusinessRegisterEntities_BusinessR~",
                        column: x => x.BusinessRegisterEntityId1,
                        principalTable: "BusinessRegisterEntities",
                        principalColumn: "BusinessRegisterEntityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRegisterEntities_BusinessRegisterEntityId1",
                table: "BusinessRegisterEntities",
                column: "BusinessRegisterEntityId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessRegisterEntities");
        }
    }
}
