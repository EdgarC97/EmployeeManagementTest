﻿using AutoMapper;
using EmployeeManagement.API.Data.Repositories;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetActiveEmployeesAsync();
            return employees
                .Where(e => e.IsActive)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToList();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null || !employee.IsActive)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdentificationNumberAsync(createEmployeeDto.IdentificationNumber);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException();
            }

            var employee = _mapper.Map<Employee>(createEmployeeDto);
            employee.CreatedAt = DateTime.Now;
            employee.IsActive = true;

            var id = await _employeeRepository.AddAsync(employee);
            employee.Id = id;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(updateEmployeeDto.Id);
            if (employee == null || !employee.IsActive)
                return null;

            if (employee.IdentificationNumber != updateEmployeeDto.IdentificationNumber)
            {
                var existingEmployee = await _employeeRepository.GetByIdentificationNumberAsync(updateEmployeeDto.IdentificationNumber);
                if (existingEmployee != null && existingEmployee.Id != updateEmployeeDto.Id)
                {
                    throw new InvalidOperationException();
                }
            }

            _mapper.Map(updateEmployeeDto, employee);
            employee.UpdatedAt = DateTime.Now;

            var success = await _employeeRepository.UpdateAsync(employee);
            if (!success)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }
    }
}
