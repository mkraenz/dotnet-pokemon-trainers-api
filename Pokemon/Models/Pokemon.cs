using System.ComponentModel.DataAnnotations;

namespace dotnettest.Pokemon.Models
{
    public class Pokemon
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int SpeciesId { get; set; }
        public Species Species { get; set; } = default!;

        [Required]
        [Range(1, 100)]
        public int Level { get; set; } = 1;

        public Guid TrainerId { get; set; }
        // TODO something is still wrong when creating a 2nd pokemon for a trainer
        public Trainer Trainer { get; set; } = default!;

        [StringLength(30, MinimumLength = 1)]
        public string? Nickname { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}