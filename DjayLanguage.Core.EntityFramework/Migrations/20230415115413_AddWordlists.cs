using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DjayLanguage.Core.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddWordlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Synonyms",
                table: "WordDefinitions",
                type: "nvarchar(900)",
                maxLength: 900,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wordlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wordlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wordlists_WordGroups_WordGroupId",
                        column: x => x.WordGroupId,
                        principalTable: "WordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wordlists_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wordlists_WordGroupId",
                table: "Wordlists",
                column: "WordGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Wordlists_WordId",
                table: "Wordlists",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wordlists");

            migrationBuilder.DropTable(
                name: "WordGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Synonyms",
                table: "WordDefinitions",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(900)",
                oldMaxLength: 900,
                oldNullable: true);
        }
    }
}
