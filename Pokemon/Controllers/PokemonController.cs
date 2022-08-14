using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("api/species")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _service;

        public PokemonController(PokemonService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Models.Pokemon> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{index}")]
        public async Task<ActionResult<Models.Pokemon>> GetOneAsync(int index)
        {
            try
            {
                Models.Pokemon? pokemon = await _service.GetByIndexAsync(index);
                return pokemon is null ? (ActionResult<Models.Pokemon>)NotFound() : (ActionResult<Models.Pokemon>)pokemon;
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

    }

}