using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync();
        Task<EmployeeViewModel?> GetEmployeeByIdAsync(int id);
        Task<EmployeeViewModel> CreateEmployeeAsync(CreateEmployeeViewModel createEmployeeViewModel);
        Task<EmployeeViewModel?> UpdateEmployeeAsync(UpdateEmployeeViewModel updateEmployeeViewModel);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeViewModel>> SearchEmployeesAsync(string searchTerm);
    }
}