using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confab.Modules.Speakers.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Speakers_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "speakers");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "speakers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "speakers");
        }
    }
}
