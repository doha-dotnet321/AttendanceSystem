using Attendence_System.Models;
using Attendence_System.Models.DTOS;

namespace Attendence_System.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddAsync(CreateEmployeeDto employee);
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<List<EmployeeDto>> GetByNameAsync(string PerfixName);
        Task<UpdateEmployeeResponse> UpdateAsync(int id,UpdateEmployeeDto employee);
        Task<DeleteEmployeeResponse> DeleteAsync(int id);
    }
}
