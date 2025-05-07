using AutoMapper;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<Employee, EmployeeDto>();

            // DTO to Entity
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}