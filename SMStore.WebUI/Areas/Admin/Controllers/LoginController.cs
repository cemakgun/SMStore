using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Models;
using System.Security.Claims; // oturum için kütüphane

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IRepository<AppUser> _repository; // Kullanıcı sorgu işlemini veritabanından yapmak için
        private readonly IValidator<AdminLoginViewModel> _validator; // FluentValidation i kullanmak için ekliyoruz
        public LoginController(IRepository<AppUser> repository, IValidator<AdminLoginViewModel> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminLoginViewModel model)
        {
            var result = await _validator.ValidateAsync(model); // FluentValidation ile geçerlilik kontrolü
            if (result.IsValid)
            {
            try
            {
                var account = _repository.Get(x => x.Email == model.Email && x.Password == model.Password && x.IsActive);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    // Eğer verilen bilgilerle eşleşen kullanıcı veritabanında varsa 
                    var claims = new List<Claim>() // kullnıcıya haklar tanımlıyoruz
                    {
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("Role", account.IsAdmin ? "Admin" : "User"), // eğer adminse admin hakkı değilse user hakkı verdik
                        new Claim("UserId", account.Id.ToString())
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin/Main");
                }
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", hata.Message + "Hatası Oluştu!");
                
            }
            }
            else
            {
                foreach (var error in result.Errors) // Eğer validasyon başarısızsa oluşan hataları result.Errors ile yakalayıp foreach döngüsüyle ekrana yollayabiliriz.
                {
                      ModelState.Remove(error.PropertyName); // asp.net core un hata mesajlarını ekranda göstermemek için 
                      ModelState.AddModelError("", error.ErrorMessage); // FluentValidation in hata mesajlarını ekranda göstermek için
                }

            }
            
            return View();
        }

        [Route("Admin/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(); // Çıkış yap
            return RedirectToAction("Index"); // Login e yönlendir
        }

    }
}
