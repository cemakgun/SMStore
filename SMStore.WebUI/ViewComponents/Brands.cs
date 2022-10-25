﻿using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

namespace SMStore.WebUI.ViewComponents
{
    public class Brands : ViewComponent
    {
        private readonly IRepository<Brand> _repository;

        public Brands(IRepository<Brand> repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _repository.GetAllAsync(c => c.IsActive);
            return View(brands);
        }

    }
}
