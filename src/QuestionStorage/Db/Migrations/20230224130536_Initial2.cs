using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.QuestionStorage.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TextOnlyFormulation",
                table: "TextOnlyFormulation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeTextAnswerDefinition",
                table: "FreeTextAnswerDefinition");

            migrationBuilder.RenameTable(
                name: "TextOnlyFormulation",
                newName: "TextOnlyFormulations");

            migrationBuilder.RenameTable(
                name: "FreeTextAnswerDefinition",
                newName: "FreeTextAnswerDefinitions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextOnlyFormulations",
                table: "TextOnlyFormulations",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeTextAnswerDefinitions",
                table: "FreeTextAnswerDefinitions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TextOnlyFormulations",
                table: "TextOnlyFormulations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeTextAnswerDefinitions",
                table: "FreeTextAnswerDefinitions");

            migrationBuilder.RenameTable(
                name: "TextOnlyFormulations",
                newName: "TextOnlyFormulation");

            migrationBuilder.RenameTable(
                name: "FreeTextAnswerDefinitions",
                newName: "FreeTextAnswerDefinition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextOnlyFormulation",
                table: "TextOnlyFormulation",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeTextAnswerDefinition",
                table: "FreeTextAnswerDefinition",
                column: "QuestionId");
        }
    }
}
