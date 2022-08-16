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
    }
}