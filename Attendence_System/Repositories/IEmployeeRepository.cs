using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;

namespace Attendence_System.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetByNameAsync(string PerfixName);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(Employee employee);
    }

}
