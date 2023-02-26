using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.QuestionStorage.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddingOneTextChoiceAnswerDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OneTextChoiceAnswerDefinitions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NotesForPlayers = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Variants = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CorrectVariant = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTextChoiceAnswerDefinitions", x => x.QuestionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneTextChoiceAnswerDefinitions");
        }
    }
}
