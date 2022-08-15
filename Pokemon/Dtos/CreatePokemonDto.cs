using System.ComponentModel.DataAnnotations;

namespace dotnettest.Pokemon.Dtos
{
    public class CreatePokemonDto
    {
        public int SpeciesId { get; set; }
        public Guid TrainerId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Level { get; set; }

        [StringLength(30, MinimumLength = 1)]
        public string? Nickname { get; set; }
    }
}