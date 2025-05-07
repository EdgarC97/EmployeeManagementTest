using System.Data;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.API.Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlCommand CreateCommand(string commandText, CommandType commandType = CommandType.Text, SqlConnection? connection = null)
        {
            var conn = connection ?? CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (connection == null)
                cmd.Connection.Open();

            return cmd;
        }

        public async Task<SqlCommand> CreateCommandAsync(string commandText, CommandType commandType = CommandType.Text, SqlConnection? connection = null)
        {
            var conn = connection ?? CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (connection == null)
                await cmd.Connection.OpenAsync();

            return cmd;
        }
    }
}