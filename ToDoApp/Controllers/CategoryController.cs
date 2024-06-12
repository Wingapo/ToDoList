using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ServiceFactory _serviceFactory;

        private readonly StorageSource _source;

        public CategoryController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _serviceFactory.GetService<ICategoryService>(_source).Add(category);
            }
            return RedirectToAction("Index", "Note");
        }
        public IActionResult Delete(int id)
        {
            _serviceFactory.GetService<ICategoryService>(_source).Delete(id);
            return RedirectToAction("Index", "Note");
        }
    }
}