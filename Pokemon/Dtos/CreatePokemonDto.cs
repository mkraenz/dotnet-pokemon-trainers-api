using System.ComponentModel.DataAnnotations;

using m = dotnettest.Pokemon.Models;

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