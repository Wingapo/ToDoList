using Dapper;
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

        public int Add(Category category)
        {
            string query = @"
                INSERT INTO Categories (Name)
                OUTPUT inserted.Id
                VALUES (@Name)";

            return _connection.QuerySingle<int>(query, category);
        }

        public void Delete(int id) =>
            _connection.Execute($"DELETE Categories WHERE Id = {id}");

        public Category? Get(int id)
        {
            string query = $@"
                SELECT Id, Name
                FROM Categories
                WHERE Id = {id}";

            return _connection.QueryFirstOrDefault<Category>(query);
        }

        public IEnumerable<Category> GetAll()
        {
            string query = @"
                SELECT Id, Name
                FROM Categories";

            return _connection.Query<Category>(query);
        }
    }
}