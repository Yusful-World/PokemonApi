using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PhotoController : Controller
    { 
        private readonly ImageService _imageService;
        private readonly AppDbContext _context;

        public PhotoController(AppDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult GetPhotos()
        {
            var photos = _context.Photos.ToList();
            return Ok(photos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            if (photo == null)
            {
                return NotFound("Photo does not exist.");
            }
            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, Photo photo, IFormFile? newImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPhoto = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (existingPhoto == null)
            {
                return NotFound();
            }

            //if (newImage != null)
            //{
            //    _imageService.DeleteImage(existingPhoto.ImageUrl);
            //    existingPhoto.ImageUrl = await _imageService.SaveImageAsync(newImage);
            //}

            _context.Photos.Update(existingPhoto);
            await _context.SaveChangesAsync();

            return Ok("Photo was updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            //_imageService.DeleteImage(photo.ImageUrl);
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Photo deleted successfully!" });
        }
    }
}
