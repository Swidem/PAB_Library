using AutoMapper;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Exceptions;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Services
{
    public class BookService : IBookService
    {
        private readonly ILibraryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(ILibraryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<BookDto> GetAll()
        {
            var books = _unitOfWork.BookRepository.GetAll();
            return _mapper.Map<List<BookDto>>(books);
        }

        public BookDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }
            var book = _unitOfWork.BookRepository.Get(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            return _mapper.Map<BookDto>(book);
        }

        public int Create(CreateBookDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Invalid book data");
            }

            var book = _mapper.Map<Book>(dto);

            _unitOfWork.BookRepository.Insert(book);
            _unitOfWork.Commit();

            return book.BookId;
        }

        public void Update(UpdateBookDto dto)
        {
            var book = _unitOfWork.BookRepository.Get(dto.BookId);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            _mapper.Map(dto, book);

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var book = _unitOfWork.BookRepository.Get(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            _unitOfWork.BookRepository.Delete(book);
            _unitOfWork.Commit();
        }
    }
}