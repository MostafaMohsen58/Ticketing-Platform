using Tixora.Settings;

namespace Tixora.Services
{
    public class FileService
    {
        private readonly string _imagepath;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagepath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public async Task<string> SaveCoverAsync(IFormFile photo)
        {
            var ImgName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            var path = Path.Combine(_imagepath, ImgName);

            using var stream = File.Create(path);
            await photo.CopyToAsync(stream);
            return ImgName;

        }
        public void DeleteCover(string ImgName)
        {
            var path = Path.Combine(_imagepath, ImgName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
