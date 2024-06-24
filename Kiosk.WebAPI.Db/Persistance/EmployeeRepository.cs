using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Persistance
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public EmployeeRepository(LibraryDbContext context) : base(context)
        {
            _libraryDbContext = context;
        }

        public int GetMaxId()
        {
            return _libraryDbContext.Employees.Max(x => x.EmployeeId);
        }
    }
}
