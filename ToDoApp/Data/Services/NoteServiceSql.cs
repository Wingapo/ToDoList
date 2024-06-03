using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class NoteServiceSql : INoteService
    {
        private readonly SqlConnection _connection;

        public NoteServiceSql(SqlContext context)
        {
            _connection = context.Connection;
        }

        public void Add(Note note)
        {
            note.Id = (int)_connection.Insert(note);

            foreach (var noteCategory in note.Note_Categories)
            {
                noteCategory.NoteId = note.Id;
            }

            _connection.Insert(note.Note_Categories);
        }

        public void Delete(int id)
        {
            Note? note = Get(id);

            if (note != null)
            {
                _connection.Delete(note);
            }
        }

        public Note? Get(int id)
        {
            Note? note = _connection.Get<Note>(id);

            if (note != null)
            {
                string query = $@"
                    SELECT c.Id, c.Name
                    FROM Note_Category nc
                    INNER JOIN Categories c ON nc.CategoryId = c.Id
                    WHERE nc.NoteId = {note.Id}";

                var categories = _connection.Query<Category>(query);

                foreach (var category in categories)
                {
                    note.Note_Categories.Add(new Note_Category()
                    {
                        NoteId = note.Id,
                        CategoryId = category.Id,
                        Category = category
                    });
                }
            }
            return note;
        }

        public IEnumerable<Note> GetAll()
        {
            string query = $@"
                SELECT n.Id, n.Title, n.Description, n.Status, n.Deadline, c.Id, c.Name
                FROM Notes n
                LEFT JOIN Note_Category nc ON nc.NoteId = n.Id
                LEFT JOIN Categories c ON c.Id = nc.CategoryId";

            var notes = _connection.Query<Note, Category, Note>(query, (n, c) =>
            {
                if (c != null)
                {
                    n.Note_Categories.Add(new Note_Category()
                    {
                        NoteId = n.Id,
                        CategoryId = c.Id,
                        Category = c
                    });
                }
                return n;
            });

            var result = notes.GroupBy(n => n.Id).Select(g =>
            {
                var note = g.First();

                note.Note_Categories = g.TakeWhile(n => n.Note_Categories.Count > 0)
                                        .Select(n => n.Note_Categories.First()).ToList();

                return note;
            });

            return result;
        }

        public void Update(int id, Note newNote)
        {
            Note? note = Get(id);

            if (note != null)
            {
                newNote.Id = note.Id;
                _connection.Update(newNote);
            }
        }
    }
}