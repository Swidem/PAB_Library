using Kiosk.WebAPI.Dto;

namespace Kiosk.WebAPI.Db.Services
{
    public interface ILoanService
    {
        List<LoanDto> GetAll();
        LoanDto GetById(int id);
        int Create(CreateLoanDto dto);
        void Update(UpdateLoanDto dto);
        void Delete(int id);
    }
}
