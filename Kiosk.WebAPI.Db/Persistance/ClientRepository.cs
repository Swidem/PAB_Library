using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Persistance
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public ClientRepository(LibraryDbContext context) : base(context)
        {
            _libraryDbContext = context;
        }

        public int GetMaxId()
        {
            return _libraryDbContext.Clients.Max(x => x.ClientId);
        }
    }
}