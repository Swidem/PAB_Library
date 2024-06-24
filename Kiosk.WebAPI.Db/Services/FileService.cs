using Microsoft.AspNetCore.StaticFiles;

namespace Kiosk.WebAPI.Db.Services
{
    public class FileService : IFileService
    {
        public void UploadFile(IFormFile file)
        {
            var dir = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, "Files", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        public FileResult DownloadFile(string fileName)
        {
            var dir = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, "Files", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return new FileResult
            {
                Bytes = bytes,
                MimeType = mimeType,
                FileName = fileName
            };
        }
    }
}
