using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Services;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            if(ModelState.IsValid)
            {
                _categoryService.Add(category);
            }
            return RedirectToAction("Index", "Note");
        }
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index", "Note");
        }
    }
}
