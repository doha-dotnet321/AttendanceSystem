using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using Attendence_System.Repositories;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace Attendence_System.Services
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

        public async Task<EmployeeDto> AddAsync(CreateEmployeeDto employeeDto)
        {

            var employeeEntity = _mapper.Map<Employee>(employeeDto); // Map CreateEmployeeDto to Employee entity
            var createdEmployee = await _employeeRepository.AddAsync(employeeEntity); // Save the entity to the database
            var createdEmployeeDto = _mapper.Map<EmployeeDto>(createdEmployee);// Map the created Employee entity back to EmployeeDto
            return createdEmployeeDto; // Return the created EmployeeDto

        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employeesEntities = await _employeeRepository.GetAllAsync();
            var employessDto = _mapper.Map<List<EmployeeDto>>(employeesEntities);
            return employessDto;

        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employeeEntity = await _employeeRepository.GetByIdAsync(id); // Retrieve the Employee entity from the database
            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeDto;
        }

        public async Task<List<EmployeeDto>> GetByNameAsync(string PerfixName)
        {
            var employeesEntities = await _employeeRepository.GetByNameAsync(PerfixName);
            var employessDto = _mapper.Map<List<EmployeeDto>>(employeesEntities);
            return employessDto;

        }

        public async Task<UpdateEmployeeResponse> UpdateAsync(int id, UpdateEmployeeDto employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return new UpdateEmployeeResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Employee not found."
                };
            }
            // Map the updated properties from UpdateEmployeeDto to the existing Employee entity
            _mapper.Map(employee, existingEmployee);
            var updatedEmployee = await _employeeRepository.UpdateAsync(existingEmployee);
            var updatedEmployeeDto = _mapper.Map<UpdateEmployeeDto>(updatedEmployee);
            return new UpdateEmployeeResponse
            {
                IsSuccess = true,
                Data = updatedEmployeeDto


            };
        }
        public async Task<DeleteEmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return new DeleteEmployeeResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Employee not found."
                };
            }
            var result = await _employeeRepository.DeleteAsync(existingEmployee);
            if (!result)
            {
                return new DeleteEmployeeResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Failed to delete the employee."
                };
            }
            return new DeleteEmployeeResponse
            {
                IsSuccess = true
            };
        }
    }
}
