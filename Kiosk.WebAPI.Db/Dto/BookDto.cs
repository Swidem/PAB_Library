using Kiosk.WebAPI.Db.Models;

namespace Kiosk.WebAPI.Db.Dto
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public StatusOfBook StatusOfBook { get; set; }
    }
}
