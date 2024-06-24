using Kiosk.WebAPI.Db.Dto;

namespace Kiosk.WebAPI.Db.Services
{
    public interface IBookService
    {
        List<BookDto> GetAll();
        BookDto GetById(int id);
        int Create(CreateBookDto dto);
        void Update(UpdateBookDto dto);
        void Delete(int id);
    }
}