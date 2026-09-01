using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using AutoMapper;

namespace Attendence_System.Models.Profiles
{
    public class EmployeeProfile : Profile // inherit
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee,CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeResponse>().ReverseMap();
            CreateMap<Employee, DeleteEmployeeResponse>().ReverseMap();

        }
    }
}
