using System.Data;
using Microsoft.Data.SqlClient;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseConnection _db;

        public EmployeeRepository(DatabaseConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = new List<Employee>();

            using var cmd = await _db.CreateCommandAsync(
                "SELECT Id, FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, UpdatedAt, IsActive FROM Employees");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployeeFromReader(reader));
            }

            return employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            using var cmd = await _db.CreateCommandAsync(
                "SELECT Id, FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, UpdatedAt, IsActive FROM Employees WHERE Id = @Id");

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapEmployeeFromReader(reader);
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            var employees = new List<Employee>();

            using var cmd = await _db.CreateCommandAsync(
                "SELECT Id, FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, UpdatedAt, IsActive FROM Employees WHERE IsActive = 1");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(MapEmployeeFromReader(reader));
            }

            return employees;
        }

        public async Task<Employee?> GetByIdentificationNumberAsync(string identificationNumber)
        {
            using var cmd = await _db.CreateCommandAsync(
                "SELECT Id, FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, UpdatedAt, IsActive FROM Employees WHERE IdentificationNumber = @IdentificationNumber");

            cmd.Parameters.AddWithValue("@IdentificationNumber", identificationNumber);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapEmployeeFromReader(reader);
            }

            return null;
        }

        public async Task<int> AddAsync(Employee employee)
        {
            using var cmd = await _db.CreateCommandAsync(@"
                INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, DateOfBirth, IdentificationNumber, CreatedAt, IsActive)
                VALUES (@FirstName, @LastName, @Address, @PhoneNumber, @DateOfBirth, @IdentificationNumber, @CreatedAt, @IsActive);
                SELECT SCOPE_IDENTITY();");

            AddEmployeeParameters(cmd, employee);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            using var cmd = await _db.CreateCommandAsync(@"
                UPDATE Employees
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Address = @Address,
                    PhoneNumber = @PhoneNumber,
                    DateOfBirth = @DateOfBirth,
                    IdentificationNumber = @IdentificationNumber,
                    UpdatedAt = @UpdatedAt,
                    IsActive = @IsActive
                WHERE Id = @Id");

            AddEmployeeParameters(cmd, employee);
            cmd.Parameters.AddWithValue("@Id", employee.Id);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Soft delete - just update IsActive to false
            using var cmd = await _db.CreateCommandAsync(@"
                UPDATE Employees
                SET IsActive = 0,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id");

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        private void AddEmployeeParameters(SqlCommand cmd, Employee employee)
        {
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
            cmd.Parameters.AddWithValue("@IdentificationNumber", employee.IdentificationNumber);
            cmd.Parameters.AddWithValue("@CreatedAt", employee.CreatedAt);
            cmd.Parameters.AddWithValue("@UpdatedAt", employee.UpdatedAt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
        }

        private Employee MapEmployeeFromReader(SqlDataReader reader)
        {
            return new Employee
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                IdentificationNumber = reader.GetString(reader.GetOrdinal("IdentificationNumber")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}