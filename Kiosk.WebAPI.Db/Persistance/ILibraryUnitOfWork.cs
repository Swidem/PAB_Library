using Kiosk.WebAPI.Db.Persistance;

namespace Kiosk.WebAPI.Persistance
{
    public interface ILibraryUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IClientRepository ClientRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ILoanRepository LoanRepository { get; }
        void Commit();
    }
}