using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnettest.Migrations.Pokemon
{
    public partial class renameOrderToIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Pokemons",
                newName: "Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Pokemons",
                newName: "Order");
        }
    }
}
