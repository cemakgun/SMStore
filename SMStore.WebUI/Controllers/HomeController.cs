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
        private readonly IRepository<Slider> _repositorySlider;
        private readonly IRepository<Product> _repositoryProduct;

        public HomeController(ILogger<HomeController> logger, IRepository<Contact> repositoryContact, IRepository<Slider> repositorySlider, IRepository<Product> repositoryProduct)
        {
            _logger = logger;
            _repositoryContact = repositoryContact;
            _repositorySlider = repositorySlider;
            _repositoryProduct = repositoryProduct;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel();
            model.Sliders = await _repositorySlider.GetAllAsync();
            model.Products = await _repositoryProduct.GetAllAsync(p => p.IsActive && p.IsHome); // buraya konan şart veriyi bir kere database den çeker örnek 7000 ürün var o zaman hepsi gelir filtre koyduk.
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
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

        [Route("iletisim"), HttpPost]
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
                        TempData["mesaj"] = "<div class='alert alert-success'>Mesajınız İletilmiştir.. Teşekkür Ederiz...</div>";
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}