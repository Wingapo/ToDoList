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

        private readonly StorageSource _source;

        public NoteController(ILogger<NoteController> logger, ServiceFactory serviceFactory)
        {
            _logger = logger;
            _serviceFactory = serviceFactory;
            _source = StorageSource.Session;
        }

        public IActionResult Index()
        {
            var notes = _serviceFactory.GetService<INoteService>(_source).GetAll();
            var categories = _serviceFactory.GetService<ICategoryService>(_source).GetAll();

            NoteCategoryViewModel viewModel = new NoteCategoryViewModel()
            {
                Notes = notes.Reverse().OrderBy(n => n.Status),
                Categories = categories.OrderBy(c => c.Name),
                StorageType = _serviceFactory.GetStorageTypeOrDefault(_source)
            };

            return View(viewModel);
        }

        public IActionResult Complete(int id)
        {
            INoteService noteService = _serviceFactory.GetService<INoteService>(_source);

            Note? note = noteService.Get(id);
            if (note != null)
            {
                note.Status = NoteStatus.Completed;
                noteService.Update(note);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            _serviceFactory.GetService<INoteService>(_source).Add(note);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _serviceFactory.GetService<INoteService>(_source).Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
