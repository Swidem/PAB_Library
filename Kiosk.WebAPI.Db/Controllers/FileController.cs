using Kiosk.WebAPI.Db.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kiosk.WebAPI.Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult DownloadFile([FromQuery] string fileName)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie pliku: {fileName}");
            var fileResult = _fileService.DownloadFile(fileName);
            if (fileResult == null)
            {
                _logger.LogWarning($"Plik nie znaleziony: {fileName}");
                return NotFound();
            }

            _logger.LogDebug($"Zakończono pobieranie pliku: {fileName}");
            return File(fileResult.Bytes, fileResult.MimeType, fileResult.FileName);
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogWarning("Próba przesłania pustego pliku");
                return BadRequest();
            }

            _logger.LogDebug($"Rozpoczęto przesyłanie pliku: {file.FileName}");
            _fileService.UploadFile(file);
            _logger.LogDebug($"Zakończono przesyłanie pliku: {file.FileName}");
            return Ok();
        }
    }
}