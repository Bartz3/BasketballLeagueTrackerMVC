using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deleteArticleImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticaleImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonStatistics",
                table: "SeasonStatistics");

            migrationBuilder.DropIndex(
                name: "IX_SeasonStatistics_TeamId",
                table: "SeasonStatistics");

            migrationBuilder.DropColumn(
                name: "SeasonStatisticsId",
                table: "SeasonStatistics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonStatistics",
                table: "SeasonStatistics",
                columns: new[] { "TeamId", "LeagueId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonStatistics",
                table: "SeasonStatistics");

            migrationBuilder.AddColumn<int>(
                name: "SeasonStatisticsId",
                table: "SeasonStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonStatistics",
                table: "SeasonStatistics",
                column: "SeasonStatisticsId");

            migrationBuilder.CreateTable(
                name: "ArticaleImages",
                columns: table => new
                {
                    ArticleImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticaleImages", x => x.ArticleImageId);
                    table.ForeignKey(
                        name: "FK_ArticaleImages_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonStatistics_TeamId",
                table: "SeasonStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticaleImages_ArticleId",
                table: "ArticaleImages",
                column: "ArticleId");
        }
    }
}
