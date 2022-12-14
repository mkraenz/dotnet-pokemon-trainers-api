using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnettest.Pokemon.Models
{
    public class Pokemon
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int SpeciesId { get; set; }
        public Species Species { get; set; } = default!;

        public int Level { get; set; } = 1;

        public Guid TrainerId { get; set; }
        [JsonIgnore]
        public Trainer Trainer { get; set; } = default!;

        [StringLength(30, MinimumLength = 1)]
        public string? Nickname { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<Team> Teams { get; set; } = default!;
    }
}