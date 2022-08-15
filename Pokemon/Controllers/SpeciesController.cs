using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("api/species")]
    public class SpeciesController : ControllerBase
    {
        private readonly SpeciesService _service;

        public SpeciesController(SpeciesService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Species> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Species>> GetOneAsync(int id)
        {
            try
            {
                Species? species = await _service.GetByIdAsync(id);
                return species is null ? NotFound() : species;
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

    }

}