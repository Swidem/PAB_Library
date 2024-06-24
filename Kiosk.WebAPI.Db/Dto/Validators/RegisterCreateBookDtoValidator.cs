using FluentValidation;
using Kiosk.WebAPI.Persistance;

namespace Kiosk.WebAPI.Db.Dto.Validators
{
    public class RegisterCreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        private readonly ILibraryUnitOfWork _unitOfWork;

        public RegisterCreateBookDtoValidator(ILibraryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(b => b.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Must(BeUniqueTitle).WithMessage("Title must be unique");
        }

        private bool BeUniqueTitle(string title)
        {
            return !_unitOfWork.BookRepository.Find(b => b.Title == title).Any();
        }
    }
}