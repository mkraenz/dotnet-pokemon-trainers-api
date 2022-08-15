using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnettest.Pokemon.Models
{
    public class Pokemon
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int SpeciesId { get; set; }
        public Species Species { get; set; } = default!;

        [Required]
        [Range(1, 100)]
        public int Level { get; set; } = 1;

        [Required]
        public Guid TrainerId { get; set; }
        [JsonIgnore]
        public Trainer Trainer { get; set; } = default!;

        [StringLength(30, MinimumLength = 1)]
        public string? Nickname { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}