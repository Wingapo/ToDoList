using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _db;

        public NoteService(AppDbContext db)
        {
            _db = db;
        }

        public void Add(Note note)
        {
            _db.Add(note);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Note? note = _db.Notes.FirstOrDefault(n => n.Id == id);
            if (note != null)
            {
                _db.Notes.Remove(note);
                _db.SaveChanges();
            }
        }

        public Note? Get(int id) => _db.Notes.FirstOrDefault(n => n.Id == id);

        public async Task<IEnumerable<Note>> GetAll()
        {
            return await _db.Notes
                .Include(n => n.Note_Category)
                .ThenInclude(nc => nc.Category)
                .ToListAsync();
        }

        public void Update(int id, Note newNote)
        {
            Note? note = Get(id);
            if (note != null)
            {
                note = newNote;
                _db.SaveChanges();
            }
        }
    }
}
