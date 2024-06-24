using Kiosk.WebAPI.Db.Persistance;

namespace Kiosk.WebAPI.Persistance
{
    // implementacja Unit of Work
    public class LibraryUnitOfWork : ILibraryUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public IBookRepository BookRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public ILoanRepository LoanRepository { get; }

        public LibraryUnitOfWork(LibraryDbContext context, IBookRepository bookRepository, IClientRepository clientRepository, IEmployeeRepository employeeRepository, ILoanRepository loanRepository)
        {
            _context = context;
            BookRepository = bookRepository;
            ClientRepository = clientRepository;
            EmployeeRepository = employeeRepository;
            LoanRepository = loanRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}