using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public interface INoteService
    {
        public void Add(Note note);
        public Note? Get(int id);
        public Task<IEnumerable<Note>> GetAll();
        public void Update(int id, Note newNote);
        public void Delete(int id);
    }
}
