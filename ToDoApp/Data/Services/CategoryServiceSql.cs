using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class CategoryServiceSql : ICategoryService
    {
        private readonly SqlConnection _connection;
        public CategoryServiceSql(SqlContext context)
        {
            _connection = context.Connection;
        }

        public void Add(Category category)
        {
            _connection.Insert(category);
        }

        public void Delete(int id)
        {
            Category? category = Get(id);

            if (category != null)
            {
                _connection.Delete(category);
            }
        }

        public Category? Get(int id)
        {
            return _connection.Get<Category>(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _connection.GetAll<Category>();
        }
    }
}