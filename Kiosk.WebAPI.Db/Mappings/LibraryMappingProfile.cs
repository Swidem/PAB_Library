using AutoMapper;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Db.Mappings
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            // Mappery dla Book
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>().ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dest => dest.StatusOfBook, opt => opt.MapFrom(src => src.StatusOfBook));

            // Mappery dla Client
            CreateMap<Client, ClientDto>();
            CreateMap<CreateClientDto, Client>();

            // Mappery dla Employee
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();

            // Mappery dla Loan
            CreateMap<Loan, LoanDto>();
            CreateMap<CreateLoanDto, Loan>();
        }
    }
}