using System.ComponentModel.DataAnnotations;

namespace dotnettest.Pokemon.Dtos
{
    public class CreateTeamDto
    {
        [Required]
        public Guid TrainerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";

        // public Guid? FirstPokemonId { get; set; }
        // public Guid? SecondPokemonId { get; set; }
        // public Guid? ThirdPokemonId { get; set; }
        // public Guid? FourthPokemonId { get; set; }
        // public Guid? FifthPokemonId { get; set; }
        // public Guid? SixthPokemonId { get; set; }
    }
}