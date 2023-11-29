using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antonyms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicWordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antonyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicWordDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultPhoneticsText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPronounceLnkPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicWordDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneticAudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PronounceLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicWordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneticAudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Synonyms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicWordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeech",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicWordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeech", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeech_BasicWordDetails_BasicWordDetailsId",
                        column: x => x.BasicWordDetailsId,
                        principalTable: "BasicWordDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntonymsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SynonymsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Antonyms_AntonymsId",
                        column: x => x.AntonymsId,
                        principalTable: "Antonyms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Words_Synonyms_SynonymsId",
                        column: x => x.SynonymsId,
                        principalTable: "Synonyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefinitionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Example = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartOfSpeechId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BasicWordDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Definitions_PartOfSpeech_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalTable: "PartOfSpeech",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_PartOfSpeechId",
                table: "Definitions",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeech_BasicWordDetailsId",
                table: "PartOfSpeech",
                column: "BasicWordDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_AntonymsId",
                table: "Words",
                column: "AntonymsId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_SynonymsId",
                table: "Words",
                column: "SynonymsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");

            migrationBuilder.DropTable(
                name: "PhoneticAudios");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "PartOfSpeech");

            migrationBuilder.DropTable(
                name: "Antonyms");

            migrationBuilder.DropTable(
                name: "Synonyms");

            migrationBuilder.DropTable(
                name: "BasicWordDetails");
        }
    }
}
