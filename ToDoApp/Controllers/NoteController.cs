using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly ServiceFactory _serviceFactory;

        public NoteController(ILogger<NoteController> logger, ServiceFactory serviceFactory)
        {
            _logger = logger;
            _serviceFactory = serviceFactory;
        }

        public IActionResult Index()
        {
            StorageType storageType = StorageType.Sql;

            if (HttpContext.Session.TryGetValue(nameof(StorageType), out _))
            {
                storageType = (StorageType)HttpContext.Session.GetInt32(nameof(StorageType))!;
            }

            var notes = _serviceFactory.GetService<INoteService>().GetAll();
            var categories = _serviceFactory.GetService<ICategoryService>().GetAll();

            NoteCategoryViewModel viewModel = new NoteCategoryViewModel()
            {
                Notes = notes.Reverse().OrderBy(n => n.Status).ToList(),
                Categories = categories.OrderBy(c => c.Name).ToList(),
                StorageType = storageType
            };

            return View(viewModel);
        }

        public IActionResult Complete(int id)
        {
            Note? note = _serviceFactory.GetService<INoteService>().Get(id);
            if (note != null)
            {
                note.Status = NoteStatus.Completed;
                _serviceFactory.GetService<INoteService>().Update(note.Id, note);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(Note note, int[] categoryIds)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var categoryId in categoryIds)
            {
                Category? category = _serviceFactory.GetService<ICategoryService>().Get(categoryId);

                if (category != null)
                {
                    note.Note_Categories.Add(new Note_Category()
                    {
                        CategoryId = category.Id,
                        Category = category
                    });
                }

            }

            _serviceFactory.GetService<INoteService>().Add(note);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _serviceFactory.GetService<INoteService>().Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
