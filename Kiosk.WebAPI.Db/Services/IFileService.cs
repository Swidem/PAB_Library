namespace Kiosk.WebAPI.Db.Services
{
    public interface IFileService
    {
        void UploadFile(IFormFile file);
        FileResult DownloadFile(string fileName);
    }

    public class FileResult
    {
        public byte[] Bytes { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
    }
}