using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowNumber",
                table: "UserCache");

            migrationBuilder.DropColumn(
                name: "Word",
                table: "UserCache");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserCache",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "CachedWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCacheId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CachedWord_UserCache_UserCacheId",
                        column: x => x.UserCacheId,
                        principalTable: "UserCache",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWord_UserCacheId",
                table: "CachedWord",
                column: "UserCacheId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWord");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserCache",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "RowNumber",
                table: "UserCache",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "UserCache",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
