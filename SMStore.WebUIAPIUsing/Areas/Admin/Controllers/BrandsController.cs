using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.WebUIAPIUsing.Utils;

namespace SMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;
        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "https://localhost:7140/Api/Brands";
        }

        // GET: BrandsController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres);
            return View(model);
        }

        // GET: BrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Brand entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
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

        // GET: BrandsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Brand entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
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

        // GET: BrandsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: BrandsController/Delete/5
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

