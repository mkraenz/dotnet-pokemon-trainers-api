using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnettest.Migrations
{
    public partial class addActiveTeamToTrainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActiveTeamId",
                table: "Trainers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_ActiveTeamId",
                table: "Trainers",
                column: "ActiveTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Teams_ActiveTeamId",
                table: "Trainers",
                column: "ActiveTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Teams_ActiveTeamId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_ActiveTeamId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ActiveTeamId",
                table: "Trainers");
        }
    }
}
