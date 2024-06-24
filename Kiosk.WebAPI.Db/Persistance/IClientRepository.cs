﻿using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Persistance
{
    public interface IClientRepository : IRepository<Client>
    {
        int GetMaxId();
    }
}