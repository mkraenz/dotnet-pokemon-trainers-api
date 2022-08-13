using System.Text.Json.Serialization;

namespace DotnetTest.Models;

public class Topping
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // jsonignore avoids cyclic structures when returned from the api, like pizza -> toppings -> pizzas
    [JsonIgnore]
    public ICollection<Pizza>? Pizzas { get; set; }
}