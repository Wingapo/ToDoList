using System.ComponentModel.DataAnnotations;
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
        public List<int> CategoryIds { get; set; } = [];
        [XmlIgnore]
        public List<Category> Categories { get; set; } = [];
    }
}