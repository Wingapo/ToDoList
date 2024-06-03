using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();
        public Category? Get(int id);
        public void Add(Category category);
        public void Delete(int id);
    }
}
