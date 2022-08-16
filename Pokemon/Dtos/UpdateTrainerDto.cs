using System.ComponentModel.DataAnnotations;

namespace dotnettest.Pokemon.Dtos
{
    public class UpdateTrainerDto
    {
        [StringLength(200, MinimumLength = 3)]
        public string? Name { get; set; }

        public Guid? ActiveTeamId { get; set; }
    }
}