namespace PokemonApi.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Power { get; set; }
        public string? ImageUrl { get; set; }
        public string Colour { get; set; }
    }
}
