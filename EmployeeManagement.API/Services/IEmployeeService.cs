using EmployeeManagement.API.DTOs;

namespace EmployeeManagement.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm);
    }
}