using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class stadiumPropsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Stadiums");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "StadiumLatitude",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StadiumLongitude",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Stadiums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_TeamId",
                table: "Stadiums",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Teams_TeamId",
                table: "Stadiums",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Teams_TeamId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_TeamId",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "StadiumLatitude",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "StadiumLongitude",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Stadiums");

            migrationBuilder.AddColumn<int>(
                name: "StadiumId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Stadiums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Stadiums",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId",
                unique: true,
                filter: "[StadiumId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Stadiums_StadiumId",
                table: "Teams",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId");
        }
    }
}
