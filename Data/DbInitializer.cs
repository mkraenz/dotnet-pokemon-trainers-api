using DotnetTest.Models;

namespace DotnetTest.Data
{
    public static class DbInitializer
    {
        public static void initialize(PizzaContext context)
        {
            if (context.Pizzas.Any() && context.Toppings.Any() && context.Sauces.Any())
            {
                return; // db already seeded
            }

            var pepperoniTopping = new Topping { Name = "Pepperoni" };
            var sausageTopping = new Topping { Name = "Sausage" };
            var hamTopping = new Topping { Name = "Ham" };
            var chickenTopping = new Topping { Name = "Chicken" };
            var pineappleTopping = new Topping { Name = "Pineapple" };

            var tomatoSauce = new Sauce { Name = "Tomato", };
            var alfredoSauce = new Sauce { Name = "Alfredo", };

            var pizzas = new Pizza[] {
                new Pizza {Name = "Meat Lovers",
                    Sauce = tomatoSauce,
                    Toppings = new List<Topping> {
                        pepperoniTopping,
                        sausageTopping,
                        hamTopping,
                        chickenTopping,
                    }
                },
                new Pizza { Name = "Hawaiian",
                Sauce = tomatoSauce,
                Toppings = new List<Topping> {
                    hamTopping,
                    pineappleTopping
                }}
            };

            context.AddRange(pizzas);
            context.SaveChanges();

        }
    }
}