using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;

namespace PokemonApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Pokemon> Pokemons { get; set; } 
        public DbSet<Photo> Photos { get; set; } 
        
    }
}
