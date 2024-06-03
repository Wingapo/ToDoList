using Microsoft.Data.SqlClient;

namespace ToDoApp.Data
{
    public class SqlContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlConnection Connection { get; }

        public SqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection")!;

            Connection = new SqlConnection(_connectionString);
        }
    }
}
