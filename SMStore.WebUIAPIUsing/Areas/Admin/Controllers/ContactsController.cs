using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public ContactsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7140/Api/Contacts";
        }

        // GET: ContactsController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Contact>>(_apiAdres);
            return View(model);
        }

        // GET: ContactsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Contact entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View();
        }

        // GET: ContactsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Contact>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Contact entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, entity);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View();
        }

        // GET: ContactsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Contact>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                var sonuc = await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                if (sonuc.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Kayıt Silinemedi!");
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }
    }
}