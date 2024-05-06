using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAll();
        public void Add(Category category);
        public void Delete(int id);
    }
}
