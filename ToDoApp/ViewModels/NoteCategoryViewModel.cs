using ToDoApp.Data.Enums;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class NoteCategoryViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public Note Note { get; set; }
        public Category Category { get; set; }

        public int[] CategoryIds { get; set; }

        public StorageType StorageType { get; set; }
    }
}