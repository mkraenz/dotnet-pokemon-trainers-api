using Microsoft.AspNetCore.Mvc;
using TsttPokemon.Models;
using TsttPokemon.Services;

namespace TsttPokemon.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _service;

    public PokemonController(PokemonService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Pokemon> getAll()
    {
        return _service.GetAll();
    }

}
