
using Attendence_System.DATA;
using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Attendence_System.Repositories
    
{
    public class EmployeeRepository : IEmployeeRepository
        
    {
        private readonly AppDbContext _Context;
        public EmployeeRepository(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            _Context.Employees.Add(employee);   // Add the employee to the DbSet
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {

            var employees = await  _Context.Employees
           .Where(x => !x.IsDeleted)  // Filter out deleted employees
            .ToListAsync();
            return employees;
           
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
          var EmployeeById = await _Context.Employees
                .FirstOrDefaultAsync(x => x.Id == id
                && !x.IsDeleted);
            return EmployeeById;

        }
        public async Task<List<Employee>> GetByNameAsync(string PerfixName)
        {
          var empByName = await _Context.Employees.Where(x => x.Name.Contains(PerfixName)&&!x.IsDeleted).ToListAsync();
            return empByName;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _Context.Update(employee);
            await _Context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(Employee employee)
        {
            
            employee.IsDeleted = true;  // Mark the employee as deleted (soft delete)
            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
