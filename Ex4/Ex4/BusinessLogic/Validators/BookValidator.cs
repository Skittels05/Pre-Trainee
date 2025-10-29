using Ex4.BusinessLogic.Models;
using FluentValidation;

namespace Ex4.BusinessLogic.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Название книги обязательно.")
                .MaximumLength(200).WithMessage("Название не может превышать 200 символов.");

            RuleFor(b => b.PublishedYear)
                .NotEmpty().WithMessage("Год публикации обязателен.");

            RuleFor(b => b.AuthorId)
                .GreaterThan(0).WithMessage("Автор должен быть указан.");
        }
    }
}