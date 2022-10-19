using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly IRepository<Category> _repository;

        public Categories(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _repository.GetAllAsync(c => c.IsTopMenu && c.IsActive);
            return View(categories);
        }

    }
}
