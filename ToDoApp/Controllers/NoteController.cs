using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _noteService;
        private readonly ICategoryService _categoryService;

        public NoteController(ILogger<NoteController> logger,
                              INoteService noteService,
                              ICategoryService categoryService)
        {
            _logger = logger;
            _noteService = noteService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetAll();
            ViewBag.Notes = notes.Reverse().OrderBy(x => x.Status).ToList();

            var categories = await _categoryService.GetAll();
            ViewBag.Categories = categories.ToList();
            
            return View();
        }

        public IActionResult Complete(int id)
        {
            Note? note = _noteService.Get(id);
            if (note != null)
            {
                note.Status = NoteStatus.Completed;
                _noteService.Update(note.Id, note);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(string title, string[] categories, DateTime? deadline, string description)
        {
            if (string.IsNullOrEmpty(title))
            {
                return RedirectToAction("Index");
            }

            Note note = new Note(title, deadline, description);

            if (categories.Length > 0)
            {
                foreach (string categoryId in categories)
                {
                    note.Note_Category.Add(new Note_Category(note.Id, int.Parse(categoryId)));
                }
            }

            _noteService.Add(note);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _noteService.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
