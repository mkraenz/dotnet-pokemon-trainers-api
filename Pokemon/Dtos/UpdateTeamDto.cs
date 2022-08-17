using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Dtos
{
    public class UpdateTeamDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";

        public Guid? FirstPokemonId { get; set; }
        public Guid? SecondPokemonId { get; set; }
        public Guid? ThirdPokemonId { get; set; }
        public Guid? FourthPokemonId { get; set; }
        public Guid? FifthPokemonId { get; set; }
        public Guid? SixthPokemonId { get; set; }

        [JsonIgnore]
        public List<Guid> PokemonsAsList
        {
            get
            {
                List<Guid> list = new();
                if (FirstPokemonId is not null)
                {
                    list.Add((Guid)FirstPokemonId);
                }

                if (SecondPokemonId is not null)
                {
                    list.Add((Guid)SecondPokemonId);
                }

                if (ThirdPokemonId is not null)
                {
                    list.Add((Guid)ThirdPokemonId);
                }

                if (FourthPokemonId is not null)
                {
                    list.Add((Guid)FourthPokemonId);
                }

                if (FifthPokemonId is not null)
                {
                    list.Add((Guid)FifthPokemonId);
                }

                if (SixthPokemonId is not null)
                {
                    list.Add((Guid)SixthPokemonId);
                }

                return list;
            }
        }

        public bool HasUniqueIds()
        {
            List<Guid> uniqueIds = PokemonsAsList.Distinct().ToList();
            return uniqueIds.Count == PokemonsAsList.Count;
        }

        public static UpdateTeamDto From(Team entity)
        {
            return new UpdateTeamDto()
            {
                Name = entity.Name,
                FirstPokemonId = entity.First?.Id,
                SecondPokemonId = entity.Second?.Id,
                ThirdPokemonId = entity.Third?.Id,
                FourthPokemonId = entity.Fourth?.Id,
                FifthPokemonId = entity.Fifth?.Id,
                SixthPokemonId = entity.Sixth?.Id,
            };
        }
    }
}