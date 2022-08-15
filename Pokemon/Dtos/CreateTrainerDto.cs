using System.ComponentModel.DataAnnotations;

namespace dotnettest.Pokemon.Dtos
{
    public class CreateTrainerDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(998)] // RFC5322
        public string Email { get; set; } = "";
    }
}