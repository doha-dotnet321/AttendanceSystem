using Attendence_System.Models;
using Attendence_System.Models.DTOS;
using Attendence_System.Repositories;
using Attendence_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Attendence_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto dto) // 
        {
            if(dto == null)
            {
                return BadRequest("Employee data is required.");
            }

            var addedEmployee = await _employeeService.AddAsync(dto);  // insert the employee into the database
            return Ok(addedEmployee); // return the added employee as a response
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            if (employees == null)
                return BadRequest("No employees found");
            return Ok(employees);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest($"Employee with ID {id} not found.");
            }
            return Ok(employee);
        }
        [HttpGet("GetByName")]
        public async Task<IActionResult>GetByNameAsync(string PerfixName)
        {
            var employees = await _employeeService.GetByNameAsync(PerfixName);
            if (employees == null || employees.Count == 0)
            {
                return NotFound($"No employees found with name containing '{PerfixName}'.");
            }
            return Ok(employees);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto employeeDto)
        {
            var response = await _employeeService.UpdateAsync(id, employeeDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            return Ok(response.Data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await _employeeService.DeleteAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            return Ok(response.IsSuccess);
        }

    }
}
