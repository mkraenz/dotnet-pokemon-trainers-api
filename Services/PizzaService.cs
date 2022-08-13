using DotnetTest.Data;
using DotnetTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTest.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetPizzas()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas.Include(p => p.Toppings).Include(p => p.Sauce).AsNoTracking().SingleOrDefault(p => p.Id == id);
    }

    public Pizza Create(Pizza pizza)
    {
        _context.Pizzas.Add(pizza);
        _context.SaveChanges();
        return pizza;
    }

    public void DeleteById(int id)
    {
        var pizza = GetById(id);
        if (pizza is not null)
        {
            _context.Remove(pizza);
            _context.SaveChanges();

        }
    }

    public void UpdateSauce(int id, int sauceId)
    {
        var pizza = _context.Pizzas.Find(id);
        var sauce = _context.Sauces.Find(sauceId);
        if (pizza is null || sauce is null)
        {
            throw new InvalidOperationException("Pizza or sauce not found");
        }
        pizza.Sauce = sauce;
        _context.SaveChanges();
    }

    public void AddTopping(int id, int toppingId)
    {
        var pizza = _context.Pizzas.Find(id);
        var topping = _context.Toppings.Find(toppingId);
        if (pizza is null || topping is null)
        {
            throw new InvalidOperationException("Pizza or topping not found");
        }
        if (pizza.Toppings is null)
        {
            pizza.Toppings = new List<Topping>();
        }
        pizza.Toppings.Add(topping);
        _context.SaveChanges();
    }
}