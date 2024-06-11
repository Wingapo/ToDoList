using System.Xml;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class NoteServiceXml : INoteService
    {
        private readonly XmlContext _context;
        private readonly XmlNode _noteRoot;

        public NoteServiceXml(XmlContext context)
        {
            _context = context;
            _noteRoot = _context.Document.SelectSingleNode("data/Notes")!;
        }

        public int Add(Note note)
        {
            XmlNode LastNoteId = _context.Document.SelectSingleNode("data/LastId/Note")!;

            int id = int.Parse(LastNoteId.InnerText) + 1;

            note.Id = id;
            LastNoteId.InnerText = id.ToString();

            _Add(note);
            return id;
        }

        private void _Add(Note note)
        {
            XmlElement noteXml = _context.Serialize(note)!;
            XmlNode node = _noteRoot.OwnerDocument!.ImportNode(noteXml, true);

            XmlNode categoriesIdXml = _context.Document.CreateNode(XmlNodeType.Element, "Categories", null);
            int[] categoriesId = note.Note_Categories.Select(nc => nc.CategoryId).ToArray();

            foreach (int categoryId in categoriesId)
            {
                XmlElement categoryIdXml = _context.Document.CreateElement("Id");
                categoryIdXml.InnerText = categoryId.ToString();

                categoriesIdXml.AppendChild(categoryIdXml);
            }
            node.AppendChild(categoriesIdXml);

            _noteRoot.AppendChild(node);

            _context.Save();
        }

        public void Delete(int id)
        {
            XmlNode? noteXml = _noteRoot.SelectSingleNode($"//Note[Id/text() = \"{id}\"]");

            if (noteXml != null)
            {
                _noteRoot.RemoveChild(noteXml);
            }

            _context.Save();
        }

        public Note? Get(int id)
        {
            XmlNode? noteXml = _noteRoot.SelectSingleNode($"//Note[Id/text() = \"{id}\"]");

            if (noteXml == null)
            {
                return null;
            }

            return _Get(noteXml);
        }

        private Note _Get(XmlNode noteXml)
        {
            XmlNode categoriesIdXml = noteXml.SelectSingleNode("Categories")!;
            LinkedList<int> categoriesId = new LinkedList<int>();

            foreach (XmlNode categoryIdXml in categoriesIdXml.ChildNodes)
            {
                categoriesId.AddLast(int.Parse(categoryIdXml.InnerText));
            }

            Note note = _context.Deserialize<Note>(noteXml)!;
            note.Id = int.Parse(noteXml["Id"]!.InnerText);

            XmlNode categoryRoot = _context.Document.SelectSingleNode("data/Categories")!;

            foreach (XmlNode categoryXml in categoryRoot.ChildNodes)
            {
                int categoryId = int.Parse(categoryXml["Id"]!.InnerText);
                if (!categoriesId.Contains(categoryId))
                {
                    continue;
                }

                categoriesId.Remove(categoryId);
                note.Note_Categories.Add(new Note_Category()
                {
                    NoteId = note.Id,
                    CategoryId = categoryId,
                    Category = _context.Deserialize<Category>(categoryXml)!
                });
            }
            return note;
        }

        public IEnumerable<Note> GetAll()
        {
            List<Note> notes = new List<Note>();

            foreach (XmlNode noteXml in _noteRoot.ChildNodes)
            {
                notes.Add(_Get(noteXml)!);
            }

            return notes;
        }

        public void Update(int id, Note newNote)
        {
            Delete(id);

            newNote.Id = id;
            _Add(newNote);
        }
    }
}