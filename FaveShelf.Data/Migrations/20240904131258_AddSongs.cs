using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaveShelf.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SongEntity_FavoriteSongId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongEntity",
                table: "SongEntity");

            migrationBuilder.RenameTable(
                name: "SongEntity",
                newName: "Songs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Songs_FavoriteSongId",
                table: "Users",
                column: "FavoriteSongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Songs_FavoriteSongId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.RenameTable(
                name: "Songs",
                newName: "SongEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongEntity",
                table: "SongEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SongEntity_FavoriteSongId",
                table: "Users",
                column: "FavoriteSongId",
                principalTable: "SongEntity",
                principalColumn: "Id");
        }
    }
}
