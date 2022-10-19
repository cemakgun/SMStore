using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository) // dependency injection
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var model = await _repository.FindAsync(id.Value); // id nin taşıdığı değeri al 

            return View();
        }
    }
}
