using FluentValidation;

namespace SMStore.WebUIAPIUsing.Models
{
    public class AdminLoginViewModelValidator : AbstractValidator<AdminLoginViewModel>
    {

        public AdminLoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Bırakılamaz!");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre Boş Bırakılamaz!").MinimumLength(3).WithMessage("Şifre Minumum 3 Karakter olmalıdır!");
        }
    }
}
