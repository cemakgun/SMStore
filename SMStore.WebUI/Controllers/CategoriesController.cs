using Microsoft.AspNetCore.Mvc;

namespace SMStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
