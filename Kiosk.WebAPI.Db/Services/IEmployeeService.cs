using Kiosk.WebAPI.Db.Dto;

namespace Kiosk.WebAPI.Db.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll();
        EmployeeDto GetById(int id);
        int Create(CreateEmployeeDto dto);
        void Update(UpdateEmployeeDto dto);
        void Delete(int id);
    }
}

