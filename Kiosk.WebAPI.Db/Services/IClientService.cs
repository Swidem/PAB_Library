using Kiosk.WebAPI.Db.Dto;

namespace Kiosk.WebAPI.Db.Services
{
    public interface IClientService
    {
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
        int Create(CreateClientDto dto);
        void Update(UpdateClientDto dto);
        void Delete(int id);
    }
}
