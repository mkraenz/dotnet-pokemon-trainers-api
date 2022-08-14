using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnettest.Migrations.Pokemon
{
    public partial class addSpriteUrlToPokemon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpriteUrl",
                table: "Pokemons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpriteUrl",
                table: "Pokemons");
        }
    }
}
