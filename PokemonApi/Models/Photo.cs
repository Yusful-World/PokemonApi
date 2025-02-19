namespace PokemonApi.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly CreatedDate { get; set; }
        public string? Author { get; set; }

        public string? ImageUrl { get; set; }
    }
}
