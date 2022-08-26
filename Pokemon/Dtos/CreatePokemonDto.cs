using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using m = dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Dtos
{
    public class CreatePokemonDto
    {
        [Required]
        [Range(1, 905)] // 905 is the last pkmn in generation 8
        [DisplayName("National Pokedex Number")]
        public int SpeciesId { get; set; }
        public Guid TrainerId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Level { get; set; } = 5;

        [StringLength(30, MinimumLength = 1)]
        public string? Nickname { get; set; }

        public static m.Pokemon ToEntity(CreatePokemonDto dto)
        {
            return new()
            {
                Level = dto.Level,
                SpeciesId = dto.SpeciesId,
                TrainerId = dto.TrainerId,
                Nickname = dto.Nickname
            };
        }
    }
}