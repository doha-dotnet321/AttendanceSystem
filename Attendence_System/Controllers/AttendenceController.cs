using Attendence_System.Models;
using Attendence_System.Models.DTOS;
using Attendence_System.Repositories;
using Attendence_System.Services;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Attendence_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase

    {
        private readonly IAttendanceService _attendenceService;
        private readonly IEmployeeService _employeeService;
        public AttendenceController(IAttendanceService attendenceService, IEmployeeService employeeService)
        {
            _attendenceService = attendenceService;
            _employeeService = employeeService;
        }

        [HttpPost("checkin/{employeeId}")]
        public async Task<IActionResult> CheckInAsync(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");

            var record = await _attendenceService.CheckInAsync(employeeId);
            if (record.IsSuccess)
            {
                return Ok(record.Data);
            }
            else
            {
                return BadRequest(record.ErrorMessage);
            }

        }
        [HttpPost("checkout/{employeeId}")]
        public async Task<IActionResult> CheckOutAsync(int employeeId) // Action method to check out an employee    
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var response = await _attendenceService.CheckOutAsync(employeeId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(

               response.ErrorMessage
           );
        }
        [HttpGet("attendance/{employeeId}")]
        public async Task<IActionResult> GetAttendanceByEmployeeIdAsync(int employeeId) // Action method to get attendance records for an employee
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var attendanceRecords = await _attendenceService.GetAttendancByEmployeeIdAsync(employeeId);
            if (attendanceRecords.IsSuccess)
            {
                return Ok(attendanceRecords.Data);
            }
            else
            {
                return BadRequest(attendanceRecords.ErrorMessage);
            }
        }
        [HttpGet("attendance/{employeeId}/{date}")]
        public async Task<IActionResult> GetAttendanceByIdAndDate(int employeeId, DateTime date) // Action method to get attendance records for an employee by date
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var attendanceRecords = await _attendenceService.GetAttendanceByIdAndDate(employeeId, date);
            if (attendanceRecords.IsSuccess)
            {
                return Ok(attendanceRecords.Data);
            }
            else
            {
                return BadRequest(attendanceRecords.ErrorMessage);
            }
        }
        [HttpGet("absent/{employeeId}")]
        public async Task<IActionResult> GetAbsentDates(int employeeId, int month, int year)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");

            var absentDates = await _attendenceService.GetAbsentDates(employeeId, month, year);
            if (absentDates.IsSuccess)
            {
                return Ok(absentDates.Data);
            }
            else
            {
                return BadRequest(absentDates.ErrorMessage);
            }
        }
        [HttpGet("present/{employeeId}")]

        public async Task<IActionResult> GetPresentDates(int employeeId, int month, int year)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var presentDates = await _attendenceService.GetPresentDates(employeeId, month, year);
            if (presentDates.Data != null && presentDates.Data.Count > 0)
            {
                return Ok(presentDates);
            }
            else
            {
                return BadRequest("No present dates found for the specified employee and month.");
            }
        }
        [HttpGet("checkin/{employeeId}/{month}/{year}")]

        public async Task<IActionResult> GetCheckInByEmployeeAndMonth(int employeeId, int month, int year)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var checkInRecords = await _attendenceService.GetCheckInByEmployeeAndMonth(employeeId, month, year);
            if (checkInRecords.IsSuccess)
            {
                return Ok(checkInRecords.Data);
            }
            else
            {
                return BadRequest(checkInRecords.ErrorMessage);
            }
        }
        [HttpGet("checkout/{employeeId}/{month}/{year}")]

        public async Task<IActionResult> GetCheckOutByEmployeeAndMonth(int employeeId, int month, int year)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                return NotFound($"Employee with id {employeeId} Not Found");
            var checkOutRecords = await _attendenceService.GetCheckOutByEmployeeAndMonth(employeeId, month, year);
            if (checkOutRecords.IsSuccess)
            {
                return Ok(checkOutRecords.Data);
            }
            else
            {
                return BadRequest(checkOutRecords.ErrorMessage);
            }
        }
    }
}





    









