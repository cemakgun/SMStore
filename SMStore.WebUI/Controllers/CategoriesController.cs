using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        // Eğer yukarıdaki gibi bir hata alırsak hatayla ilgili interface ve class i servis olarak program cs. de tanımlamamışız demektir.

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _repository.KategoriyiUrunleriliyleGetir(id);

            if (model == null) return NotFound();

            return View(model);
        }
    }
}
