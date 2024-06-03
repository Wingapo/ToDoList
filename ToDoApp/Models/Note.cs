using Dapper.Contrib.Extensions;
using System.Xml.Serialization;
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

        [XmlIgnore]
        [Computed]
        public List<Note_Category> Note_Categories { get; set; } = [];
    }
}