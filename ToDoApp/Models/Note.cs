using System.ComponentModel.DataAnnotations;
using ToDoApp.Data.Enums;

namespace ToDoApp.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public NoteStatus Status { get; set; }
        public DateTime? Deadline { get; set; }
        public List<Note_Category> Note_Category { get; set;} = new List<Note_Category>();

        public Note(string title, DateTime? deadline, string? description)
        {
            Title = title;
            Deadline = deadline;
            Description = description;
            Status = NoteStatus.Active;
        }
    }
}
