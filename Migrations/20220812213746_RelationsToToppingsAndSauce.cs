using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnettest.Migrations
{
    public partial class RelationsToToppingsAndSauce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Toppings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SauceId",
                table: "Pizzas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas",
                column: "SauceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas",
                column: "SauceId",
                principalTable: "Sauces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_Pizzas_PizzaId",
                table: "Toppings",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_Pizzas_PizzaId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "SauceId",
                table: "Pizzas");
        }
    }
}
