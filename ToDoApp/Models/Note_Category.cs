using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Note_Category
    {
        [Key]
        public int NoteId { get; set; }
        public Note Note { get; set; }

        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Note_Category(int noteId, int categoryId)
        {
            NoteId = noteId;
            CategoryId = categoryId;
        }
    }
}
