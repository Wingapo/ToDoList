using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Data.Services;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ServiceFactory _serviceFactory;

        public CategoryController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _serviceFactory.GetService<ICategoryService>().Add(category);
            }
            return RedirectToAction("Index", "Note");
        }
        public IActionResult Delete(int id)
        {
            _serviceFactory.GetService<ICategoryService>().Delete(id);
            return RedirectToAction("Index", "Note");
        }
    }
}