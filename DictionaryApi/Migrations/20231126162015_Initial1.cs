using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartOfSpeech_BasicWordDetails_BasicWordDetailsId",
                table: "PartOfSpeech");

            migrationBuilder.DropIndex(
                name: "IX_PartOfSpeech_BasicWordDetailsId",
                table: "PartOfSpeech");

            migrationBuilder.DropColumn(
                name: "BasicWordDetailsId",
                table: "PartOfSpeech");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDefinitions",
                table: "BasicWordDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDefinitions",
                table: "BasicWordDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BasicWordDetailsId",
                table: "PartOfSpeech",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeech_BasicWordDetailsId",
                table: "PartOfSpeech",
                column: "BasicWordDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartOfSpeech_BasicWordDetails_BasicWordDetailsId",
                table: "PartOfSpeech",
                column: "BasicWordDetailsId",
                principalTable: "BasicWordDetails",
                principalColumn: "Id");
        }
    }
}
