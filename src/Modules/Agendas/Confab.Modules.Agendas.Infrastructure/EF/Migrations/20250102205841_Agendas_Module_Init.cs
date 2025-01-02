using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Agendas_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "agendas");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSubmission",
                schema: "agendas",
                columns: table => new
                {
                    SubmissionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    _speakersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSubmission", x => new { x.SubmissionsId, x._speakersId });
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Speakers__speakersId",
                        column: x => x._speakersId,
                        principalSchema: "agendas",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Submissions_SubmissionsId",
                        column: x => x.SubmissionsId,
                        principalSchema: "agendas",
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSubmission__speakersId",
                schema: "agendas",
                table: "SpeakerSubmission",
                column: "_speakersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerSubmission",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "agendas");
        }
    }
}
