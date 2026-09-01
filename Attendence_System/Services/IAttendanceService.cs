using Attendence_System.Models;
using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;

namespace Attendence_System.Services
{
    public interface IAttendanceService
    {
        Task<CheckInResponseDto> CheckInAsync(int employeeId);
        //Task<AttendanceRecordDto> CheckAttendanceExist(int employeeId, DateTime today);

        Task<CheckOutResponseDto> CheckOutAsync(int employeeId);
        Task <GetAttendancByEmployeeIdRsponseDto> GetAttendancByEmployeeIdAsync(int employeeId);

        Task<GetAttendanceByIdAndDateResponseDto> GetAttendanceByIdAndDate(int employeeId, DateTime date);
        Task<AbsentResponseDto> GetAbsentDates(int employee, int month, int year);
        Task<PresentResponseDto> GetPresentDates(int employee, int month, int year);
        Task<GetAttendancByEmployeePerMonthRsponseDto> GetCheckInByEmployeeAndMonth(int employeeId, int month, int year);
        Task<GetAttendancByEmployeePerMonthRsponseDto> GetCheckOutByEmployeeAndMonth(int employeeId, int month, int year);

    }
}
