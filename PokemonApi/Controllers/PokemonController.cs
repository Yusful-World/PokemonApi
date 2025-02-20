using Microsoft.AspNetCore.Mvc;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ImageService _imageService;
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            var pokemons = _context.Pokemons.ToList();
            return Ok(pokemons);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPokemon(int id)
        {
            var pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null)
            {
                return NotFound("Pokemon not found.");
            }

            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<IActionResult> AddPokemon(Pokemon pokemon, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (image != null)
            //{
            //    string validationError = _imageService.ValidateImage(image);
            //    if (!string.IsNullOrEmpty(validationError))
            //    {
            //        return BadRequest(validationError);
            //    }

            //    pokemon.ImageUrl = await _imageService.SaveImageAsync(image);
            //}

            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemon), new { pokemon.Id }, pokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, Pokemon pokemon, IFormFile? newImage)
        {
            if (!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }
                
            var pokemonFromDb = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemonFromDb == null)
            {
            return NotFound();
            }

            //if (newImage != null)
            //{
            //    _imageService.DeleteImage(pokemonFromDb.ImageUrl);
            //    pokemonFromDb.ImageUrl = await _imageService.SaveImageAsync(newImage);
            //}

            pokemonFromDb.Name = pokemon.Name;
            pokemonFromDb.Owner = pokemon.Owner;
            pokemonFromDb.Power = pokemon.Power;
            pokemonFromDb.Colour = pokemon.Colour;
            pokemonFromDb.ImageUrl = pokemon.ImageUrl;

            await _context.SaveChangesAsync();
                
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null)
            {
                return NotFound("pokemon does not exist");
            }

            //_imageService.DeleteImage(pokemon.ImageUrl);
            _context.Pokemons.Remove(pokemon);
                
            await _context.SaveChangesAsync();
                
            return Ok(new { message = "Pokemon deleted successfully!" });
        }
        
    }
}
