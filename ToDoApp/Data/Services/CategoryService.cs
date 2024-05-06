using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public void Add(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Category? category = _db.Categories.FirstOrDefault();
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }
    }
}
