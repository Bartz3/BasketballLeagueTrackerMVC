using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedPlayerToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Players_LeaugeMVPPlayerId",
                table: "Leagues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Leagues",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Season",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LeaugeMVPPlayerId",
                table: "Leagues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Leagues",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                column: "TeamId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Players_LeaugeMVPPlayerId",
                table: "Leagues",
                column: "LeaugeMVPPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Players_LeaugeMVPPlayerId",
                table: "Leagues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Season",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaugeMVPPlayerId",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                column: "TeamId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Players_LeaugeMVPPlayerId",
                table: "Leagues",
                column: "LeaugeMVPPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
