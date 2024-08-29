using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaveShelf.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavSongToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteSongId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SongEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteSongId",
                table: "Users",
                column: "FavoriteSongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SongEntity_FavoriteSongId",
                table: "Users",
                column: "FavoriteSongId",
                principalTable: "SongEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SongEntity_FavoriteSongId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SongEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteSongId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteSongId",
                table: "Users");
        }
    }
}
