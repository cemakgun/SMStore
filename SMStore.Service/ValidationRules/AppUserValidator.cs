using FluentValidation;
using SMStore.Entities;

namespace SMStore.Service.ValidationRules
{
    public class AppUserValidator : AbstractValidator<AppUser> // AbstractValidator FluentValidation in kontrol sınıfıdır
    {

        public AppUserValidator() //ctor "constructor" shortcut
        {
            //Buradaki constructor da validasyon kurallarını belirliyoruz
            RuleFor(x => x.Name).NotEmpty(); // Kontrol ettiğimiz AppUser class ını Name özellğinin boş olmaması gerektiğini belirttik
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Bırakılamaz!");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre Boş Bırakılamaz!").MinimumLength(3).WithMessage("Şifre Minumum 3 Karakter olmalıdır!");

            // Burada rulefor ile tüm kurallarımızı koyduktan sonra AppUserValidator class ını program cs.de servis olarak tanımlıyoruz.


        }
    }
}
