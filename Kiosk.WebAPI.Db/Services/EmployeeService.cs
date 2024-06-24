using AutoMapper;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Exceptions;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILibraryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(ILibraryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public int Create(CreateEmployeeDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Invalid employee data");
            }

            var employee = _mapper.Map<Employee>(dto);

            _unitOfWork.EmployeeRepository.Insert(employee);
            _unitOfWork.Commit();

            return employee.EmployeeId;
        }

        public void Update(UpdateEmployeeDto dto)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(dto.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            _mapper.Map(dto, employee);

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Commit();
        }
    }
}