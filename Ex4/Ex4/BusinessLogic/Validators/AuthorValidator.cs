using Ex4.BusinessLogic.Models;
using FluentValidation;

namespace Ex4.BusinessLogic.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Имя автора обязательно.")
                .MaximumLength(100).WithMessage("Имя не может превышать 100 символов.");

            RuleFor(a => a.DateOfBirth)
                .NotEmpty().WithMessage("Дата рождения обязательна.")
                .LessThan(DateTime.Today).WithMessage("Дата рождения не может быть в будущем.");
        }
    }
}