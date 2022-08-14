using Microsoft.AspNetCore.Mvc;
using TsttPokemon.Models;
using TsttPokemon.Services;

namespace TsttPokemon.Controllers;

[ApiController]
[Route("api/pokemon")]
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

    [HttpGet("{index}")]
    public async Task<ActionResult<Pokemon>> getOneAsync(int index)
    {
        try
        {
            var pokemon = await _service.GetByIndexAsync(index);
            if (pokemon is null) return NotFound();
            return pokemon;

        }
        catch (System.Net.Http.HttpRequestException)
        {
            return NotFound();
        }
    }

}
