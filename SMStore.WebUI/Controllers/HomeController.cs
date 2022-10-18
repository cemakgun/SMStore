using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Models;
using System.Diagnostics;

namespace SMStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Contact> _repositoryContact;

        public HomeController(ILogger<HomeController> logger, IRepository<Contact> repositoryContact)
        {
            _logger = logger;
            _repositoryContact = repositoryContact;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            
            return View();
        }
        [Route("iletisim")]
        public IActionResult ContactUs()
        {

            return View();
        }
        [Route("Iletisim"), HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repositoryContact.AddAsync(contact);
                    var sonuc = await _repositoryContact.SaveChangesAsync();
                    if (sonuc > 0)
                    {
                        TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız İletilmiştir.. Teşeşkür Ederiz...</div>";
                        return RedirectToAction(nameof(ContactUs));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View();
        }


    }
}