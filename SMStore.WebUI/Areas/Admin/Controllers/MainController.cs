using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SMStore.WebUI.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        [Area("Admin"), Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
