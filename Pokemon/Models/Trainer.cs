using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public Guid? ActiveTeamId { get; set; }
        [ForeignKey("ActiveTeamId")]
        public Team? ActiveTeam { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; } = default!;
        public ICollection<Team> Teams { get; set; } = default!;
    }
}