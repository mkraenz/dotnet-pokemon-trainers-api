using DotnetTest.Models;
using DotnetTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTest.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetPizzas();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
        {
            return NotFound();
        }
        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
        {
            return BadRequest("provided ids do not match");
        }
        var existing = PizzaService.Get(pizza.Id);
        if (existing is null)
        {
            return NotFound();
        }
        PizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = PizzaService.Get(id);
        if (existing is null)
            return NotFound();
        PizzaService.Delete(id);
        return NoContent();
    }
}