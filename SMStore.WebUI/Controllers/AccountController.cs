using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SMStore.WebUI.Controllers
{
    public class AccountController : Controller
    {

        private readonly IRepository<Customer> _repository;

        public AccountController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(string email, string sifre)
        {
            TempData["mesaj"] = email + " - " + sifre;
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(Customer customer)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.AddAsync(customer);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }

            }
            return View();
           
        }
        public IActionResult SignOut()
        {
            return View();
        }
    }
}
