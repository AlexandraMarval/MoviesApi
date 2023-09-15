using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.Migrations
{
    /// <inheritdoc />
    public partial class Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "Movies",
                newName: "ReleaseYear");

            migrationBuilder.RenameColumn(
                name: "Descripción",
                table: "Movies",
                newName: "Descriptión");

            migrationBuilder.RenameColumn(
                name: "AñoDeLanzamiento",
                table: "Movies",
                newName: "CategoriesId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriptión = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoriesId",
                table: "Movies",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Categories_CategoriesId",
                table: "Movies",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Categories_CategoriesId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CategoriesId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Movies",
                newName: "IdCategoria");

            migrationBuilder.RenameColumn(
                name: "Descriptión",
                table: "Movies",
                newName: "Descripción");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "Movies",
                newName: "AñoDeLanzamiento");
        }
    }
}
