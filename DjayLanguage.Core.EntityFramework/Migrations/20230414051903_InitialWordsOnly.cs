using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DjayLanguage.Core.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialWordsOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Synonyms = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: true),
                    Antonyms = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordDefinitions_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordExamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordDefinitionId = table.Column<int>(type: "int", nullable: false),
                    Example = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordExamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordExamples_WordDefinitions_WordDefinitionId",
                        column: x => x.WordDefinitionId,
                        principalTable: "WordDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordDefinitions_WordId",
                table: "WordDefinitions",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordExamples_WordDefinitionId",
                table: "WordExamples",
                column: "WordDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordExamples");

            migrationBuilder.DropTable(
                name: "WordDefinitions");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
