using System.Xml.Serialization;

namespace ToDoApp.Data.Enums
{
    public enum NoteStatus
    {
        [XmlEnum("0")]
        Active,
        [XmlEnum("1")]
        Completed
    }
}