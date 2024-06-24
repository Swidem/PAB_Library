using AutoMapper;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Exceptions;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Services
{
    public class ClientService : IClientService
    {
        private readonly ILibraryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(ILibraryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<ClientDto> GetAll()
        {
            var clients = _unitOfWork.ClientRepository.GetAll();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public ClientDto GetById(int id)
        {
            var client = _unitOfWork.ClientRepository.Get(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }
            return _mapper.Map<ClientDto>(client);
        }

        public int Create(CreateClientDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Invalid client data");
            }

            var client = _mapper.Map<Client>(dto);

            _unitOfWork.ClientRepository.Insert(client);
            _unitOfWork.Commit();

            return client.ClientId;
        }

        public void Update(UpdateClientDto dto)
        {
            var client = _unitOfWork.ClientRepository.Get(dto.ClientId);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            _mapper.Map(dto, client);

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var client = _unitOfWork.ClientRepository.Get(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            _unitOfWork.ClientRepository.Delete(client);
            _unitOfWork.Commit();
        }
    }
}