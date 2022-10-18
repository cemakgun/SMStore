using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMStore.Service.Repositories;
using SMStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class AppUsersController : Controller
    {
        private readonly IRepository<AppUser> _repository; // CRUD işlemleri için gerekli interface

        public AppUsersController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        // GET: AppUsersController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _repository.GetAllAsync();
            return View(model);

        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppUser entity)
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
            return View();
        }
     
        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);

        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser entity)
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
            return View();
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _repository.FindAsync(id);
            return View(model);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, AppUser entity)
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
