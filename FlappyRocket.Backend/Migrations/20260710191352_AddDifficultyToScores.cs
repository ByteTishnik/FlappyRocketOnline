using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlappyRocket.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDifficultyToScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Scores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Scores");
        }
    }
}
