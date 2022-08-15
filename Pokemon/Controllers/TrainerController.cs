using dotnettest.Pokemon.Dtos;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Mvc;

namespace dotnettest.Pokemon.Controllers
{
    [ApiController]
    [Route("api/trainers")]
    public class TrainerController : ControllerBase
    {
        private readonly TrainerService _trainers;
        public TrainerController(TrainerService trainers)
        {
            _trainers = trainers;
        }

        [HttpGet]
        public IEnumerable<Trainer> GetAll()
        {
            return _trainers.GetAll();
        }

        // asp.net takes over validation of Guid and returns 400 if necessary
        [HttpGet("{id}")]
        public ActionResult<Trainer> Get(Guid id)
        {
            Trainer? trainer = _trainers.Get(id);
            return trainer is not null ? trainer : NotFound();
        }

        [HttpPost]
        public ActionResult<Trainer> Create(CreateTrainerDto dto)
        {
            bool emailExists = _trainers.EmailExists(dto.Email);
            if (emailExists)
            {
                return Conflict("Email already exists");
            }
            Trainer created = _trainers.Create(Trainer.From(dto));
            return created;
        }
    }
}