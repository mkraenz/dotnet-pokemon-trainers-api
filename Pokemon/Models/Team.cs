using dotnettest.Pokemon.Dtos;

namespace dotnettest.Pokemon.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TrainerId { get; set; }
        public Trainer Trainer = default!;

        public string Name { get; set; } = "";

        public ICollection<Pokemon> Members { get; set; } = new List<Pokemon>();


        public Pokemon? First => Members.ElementAtOrDefault(0);
        public Pokemon? Second => Members.ElementAtOrDefault(1);
        public Pokemon? Third => Members.ElementAtOrDefault(2);
        public Pokemon? Fourth => Members.ElementAtOrDefault(3);
        public Pokemon? Fifth => Members.ElementAtOrDefault(4);
        public Pokemon? Sixth => Members.ElementAtOrDefault(5);
    }
}