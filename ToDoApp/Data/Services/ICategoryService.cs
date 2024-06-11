using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();
        public Category? Get(int id);
        public int Add(Category category);
        public void Delete(int id);
    }
}
