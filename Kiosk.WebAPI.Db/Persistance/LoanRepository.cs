using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    // Implementacja repozytoriów specyficznych
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public LoanRepository(LibraryDbContext context) : base(context)
        {
            _libraryDbContext = context;
        }

        public int GetMaxId()
        {
            return _libraryDbContext.Loans.Max(x => x.LoanId);
        }
    }
}