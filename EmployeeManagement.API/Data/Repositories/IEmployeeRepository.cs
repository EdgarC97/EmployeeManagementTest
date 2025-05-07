using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<Employee?> GetByIdentificationNumberAsync(string identificationNumber);
        Task<int> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}