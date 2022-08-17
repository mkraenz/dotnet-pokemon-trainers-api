using System.ComponentModel.DataAnnotations;

using dotnettest.Pokemon.Models;

namespace dotnettest.Pokemon.Dtos
{
    public class CreateTeamDto
    {
        [Required]
        public Guid TrainerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";


        public static Team ToEntity(CreateTeamDto dto)
        {
            return new Team()
            {
                Name = dto.Name,
                TrainerId = dto.TrainerId,
            };
        }
    }

}