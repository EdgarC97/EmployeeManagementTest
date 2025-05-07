using EmployeeManagement.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ??
                      configuration["ApiSettings:BaseUrl"] ??
                      "https://localhost:5041/api/employees";
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(content);

            return employees ?? Enumerable.Empty<EmployeeViewModel>();
        }

        public async Task<EmployeeViewModel?> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeViewModel>(content);
        }

        public async Task<EmployeeViewModel> CreateEmployeeAsync(CreateEmployeeViewModel createEmployeeViewModel)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(createEmployeeViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeViewModel>(responseContent) ??
                   new EmployeeViewModel();
        }

        public async Task<EmployeeViewModel?> UpdateEmployeeAsync(UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(updateEmployeeViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{updateEmployeeViewModel.Id}", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeViewModel>(responseContent);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}