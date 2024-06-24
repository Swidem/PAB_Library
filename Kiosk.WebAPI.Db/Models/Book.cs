namespace Kiosk.WebAPI.Db.Models
{
        public class Book
        {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public StatusOfBook StatusOfBook { get; set; }
    }

        public enum StatusOfBook
        {
            Available,
            Borrowed,
            Reserved,
            OutOfStock
        }
    

}
