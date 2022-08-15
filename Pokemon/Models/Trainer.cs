using System.ComponentModel.DataAnnotations;

using dotnettest.Pokemon.Dtos;

using Microsoft.EntityFrameworkCore;

namespace dotnettest.Pokemon.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Trainer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(998)] // RFC5322
        public string Email { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Pokemon> Pokemons { get; set; } = default!;

        public static Trainer From(CreateTrainerDto dto)
        {
            return new Trainer()
            {
                Name = dto.Name,
                Email = dto.Email
            };
        }
    }
}