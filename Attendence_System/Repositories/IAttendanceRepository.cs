using Attendence_System.Models.Entities;

namespace Attendence_System.Repositories
{
    public interface IAttendanceRepository
    {
        Task<AttendenceRecord> CheckInAsync(AttendenceRecord record);
       
        Task<AttendenceRecord> CheckAttendanceExist(int employeeId, DateTime today);
        Task<AttendenceRecord> CheckOutAsync(int AttendanceId,DateTime CheckOutS);
        Task<List<AttendenceRecord>> GetAttendancByEmployeeIdAsync(int employeeId);
        Task<List<AttendenceRecord>> GetAttendanceByIdAndDate(int employeeId, DateTime date);
        Task<List<DateTime>> GetAbsentDates(int employee, int month, int year);

        Task <List<DateTime>>GetPresentDates(int employee, int month, int year);
        Task<List<AttendenceRecord>> GetCheckInByEmployeeAndMonth(int employeeId, int month, int year);
        Task<List<AttendenceRecord>> GetCheckOutByEmployeeAndMonth(int employeeId, int month, int year);
    }
}
