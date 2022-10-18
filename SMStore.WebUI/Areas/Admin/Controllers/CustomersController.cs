using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.WebUI.Utils;

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            return View(model);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Customer entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Customer entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Customer entity)
        {
            try
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
