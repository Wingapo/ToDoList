using Dapper.Contrib.Extensions;

namespace ToDoApp.Models
{
    [Table("Note_Category")]
    public class Note_Category
    {
        public int NoteId { get; set; }
        public int CategoryId { get; set; }

        [Computed]
        public Category Category { get; set; }
    }
}
