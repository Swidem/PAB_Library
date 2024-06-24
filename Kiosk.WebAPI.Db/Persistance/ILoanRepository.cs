using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    // interfejsy repozytoriów specyficznych
    public interface ILoanRepository : IRepository<Loan>
    {
        int GetMaxId();
    }
}
