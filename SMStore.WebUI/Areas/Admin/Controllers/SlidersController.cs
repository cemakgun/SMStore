using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Utils;


namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IRepository<Slider> _repository;

        public SlidersController(IRepository<Slider> repository)
        {
            _repository = repository;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Slider entity, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image); // FileLoaderAsync metodu bizim yazdığımız resim yükleme metodu
                    await _repository.AddAsync(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(entity);
        }

        // GET: SlidersController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model); ;
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slider entity, IFormFile? Image, bool? resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil is true) entity.Image = string.Empty;
                    if (Image is not null) entity.Image = await FileHelper.FileLoaderAsync(Image);
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(entity);
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAsync(int id, Slider entity)
        {
            try
            {
                FileHelper.FileRemover(entity.Image);
                _repository.Delete(entity);
                _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
