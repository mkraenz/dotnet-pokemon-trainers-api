using DotnetTest.Models;
using DotnetTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTest.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService service;

    public PizzaController(PizzaService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IEnumerable<Pizza> GetAll() => service.GetPizzas();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = service.GetById(id);
        if (pizza is null)
        {
            return NotFound();
        }
        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        var createdPizza = service.Create(pizza);
        return CreatedAtAction(nameof(Create), new { id = createdPizza.Id }, pizza);
    }

    [HttpPut("{id}/updatesauce")]
    public IActionResult UpdateSauce(int id, int sauceId)
    {
        var pizza = service.GetById(id);
        if (pizza is null)
        {
            return NotFound();
        }
        service.UpdateSauce(id, sauceId);
        return NoContent();
    }

    [HttpPut("{id}/addtopping")]
    public IActionResult AddTopping(int id, int toppingId)
    {
        var pizza = service.GetById(id);
        if (pizza is null)
        {
            return NotFound();
        }
        if (
            pizza.Toppings is not null &&
            pizza.Toppings.FirstOrDefault(t => t.Id == toppingId) is not null
        )
        {
            return Conflict("Topping already added to this pizza");
        }
        service.AddTopping(id, toppingId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = service.GetById(id);
        if (existing is null)
            return NotFound();
        service.DeleteById(id);
        return NoContent();
    }
}