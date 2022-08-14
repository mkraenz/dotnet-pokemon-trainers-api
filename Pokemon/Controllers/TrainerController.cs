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
            return trainer is not null ? (ActionResult<Trainer>)trainer : (ActionResult<Trainer>)NotFound();
        }

        [HttpPost]
        public ActionResult<Trainer> Create(Trainer trainer)
        {
            bool exists = _trainers.Exists(trainer.Id);
            if (exists)
            {
                return Conflict("Id already exists");
            }
            Trainer created = _trainers.Create(trainer);
            return created;
        }
    }
}