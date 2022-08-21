using System.ComponentModel.DataAnnotations;

using dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Dtos
{
    public class UpdateTrainerDto
    {
        [StringLength(200, MinimumLength = 3)]
        public string? Name { get; set; }

        public Guid? ActiveTeamId { get; set; }

        public static UpdateTrainerDto fromEntity(Trainer entity)
        {
            return new UpdateTrainerDto()
            {
                Name = entity.Name,
                ActiveTeamId = entity.ActiveTeamId
            };
        }
    }

}