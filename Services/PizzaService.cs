using DotnetTest.Models;

namespace DotnetTest.Services;

public class PizzaService
{
    static List<Pizza> Pizzas { get; }

    static int nextId = 3;

    static PizzaService()
    {
        Pizzas = new List<Pizza>(){
            new Pizza { Id = 1, Name="Classico", IsGlutenFree = false},
            new Pizza { Id =2, Name="Margherita", IsGlutenFree = true},
        };
    }

    public static List<Pizza> GetPizzas() => Pizzas;

    public static Pizza Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId;
        nextId++;
        Pizzas.Add(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
        {
            return;
        }
        Pizzas.Remove(pizza);
    }

    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
        {
            return;
        }
        Pizzas[index] = pizza;
    }
}