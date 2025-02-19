using Microsoft.AspNetCore.Hosting;

namespace PokemonApi
{
    public class ImageServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageServices(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string ValidateImage(IFormFile image)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", ".heic" };
            string imageExtension = Path.GetExtension(image.FileName.ToLower());

            if (!allowedExtensions.Contains(imageExtension))
            {
                return "Invalid file type. Only JPG, JPEG, PNG, GIF, WEBP, SVG, and HEIC files are allowed.";
            }

            return string.Empty;
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            string imageExtension = Path.GetExtension(image.FileName.ToLower());

            string imageName = Guid.NewGuid().ToString() + imageExtension;
            string imagePath = Path.Combine(rootPath, "images", imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"\\images\\{imageName}";
        }

        public void DeleteImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, imageUrl.TrimStart('/', '\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
        }

    }
}
