using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMStore.WebUIAPIUsing.Areas.Admin.Controllers
{
    public class AppUsersController : Controller
    {
     
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "";
        }

        // GET: AppUsersController
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
