using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Enums;

namespace ToDoApp.Controllers
{
    public class DataController : Controller
    {
        public IActionResult ChangeStorage(StorageType storageType)
        {
            HttpContext.Session.SetInt32(nameof(StorageType), (int)storageType);

            return RedirectToAction("Index", "Note");
        }
    }
}
