using Dapper;
using Microsoft.Data.SqlClient;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class NoteServiceSql : INoteService
    {
        private class IdPair(int id1, int id2)
        {
            public int Id1 { get; set; } = id1;
            public int Id2 { get; set; } = id2;
        }

        private readonly SqlConnection _connection;

        public NoteServiceSql(SqlContext context)
        {
            _connection = context.Connection;
        }

        public int Add(Note note)
        {
            string query = @"
                INSERT INTO Notes (Title, Description, Status, Deadline)
                OUTPUT inserted.Id
                VALUES (@Title, @Description, @Status, @Deadline)";

            note.Id = _connection.QuerySingle<int>(query, note);

            query = @"
                INSERT INTO Note_Category (NoteId, CategoryId)
                VALUES (@Id1, @Id2)";

            IEnumerable<IdPair> NoteCategory = note.CategoryIds.Select(c => new IdPair(note.Id, c));

            _connection.Execute(query, NoteCategory);

            return note.Id;
        }

        public void Delete(int id) =>
            _connection.Execute($"DELETE FROM Notes WHERE Id = {id}");

        public Note? Get(int id)
        {
            string query = $@"
                SELECT n.Id, n.Description, n.Title, n.Status, n.Deadline
                FROM Notes n
                WHERE n.Id = {id}";

            Note? note = _connection.QueryFirstOrDefault<Note>(query);

            if (note != null)
            {
                query = $@"
                    SELECT c.Id, c.Name
                    FROM Note_Category nc
                    INNER JOIN Categories c ON nc.CategoryId = c.Id
                    WHERE nc.NoteId = {note.Id}";

                note.Categories = _connection.Query<Category>(query).ToList();
                note.CategoryIds = note.Categories.Select(c => c.Id).ToList();
            }
            return note;
        }

        public IEnumerable<Note> GetAll()
        {
            string query = @"
                SELECT n.Id, n.Title, n.Description, n.Status, n.Deadline, c.Id, c.Name
                FROM Notes n
                LEFT JOIN Note_Category nc ON nc.NoteId = n.Id
                LEFT JOIN Categories c ON c.Id = nc.CategoryId";

            var notes = _connection.Query<Note, Category, Note>(query, (n, c) =>
            {
                if (c != null)
                {
                    n.Categories.Add(c);
                }
                return n;
            });

            var result = notes.GroupBy(n => n.Id).Select(g =>
            {
                Note note = g.First();

                note.Categories = g.TakeWhile(n => n.Categories.Count > 0)
                                        .Select(n => n.Categories.First()).ToList();
                
                note.CategoryIds = note.Categories.Select(c => c.Id).ToList();

                return note;
            });

            return result;
        }

        public void Update(Note note)
        {
            string query = $@"
                UPDATE Notes
                SET Title = @Title, Description = @Description, Status = @Status, Deadline = @Deadline
                WHERE Id = {note.Id}";

            _connection.Execute(query, note);
        }
    }
}