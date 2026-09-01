using Attendence_System.DATA;
using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Attendence_System.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _Context;
        private readonly TimeSpan ActiveHoureFrom = new TimeSpan(10, 0, 0); // 10:00 AM
        private readonly TimeSpan ActiveHoureTo = new TimeSpan(18, 0, 0); // 4:00 PM
        public AttendanceRepository(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<AttendenceRecord> CheckInAsync(AttendenceRecord record)
        {
            _Context.AttendenceRecords.AddAsync(record);
            await _Context.SaveChangesAsync();
            return record;
        }
        public async Task<AttendenceRecord> CheckAttendanceExist(int employeeId, DateTime today)
        {
            
                 var AttendanceRecord=  await _Context.AttendenceRecords.FirstOrDefaultAsync(n => n.EmployeeId == employeeId && n.CheckIn.Date == today.Date);
            return AttendanceRecord;
        }

        public async Task<AttendenceRecord> CheckOutAsync(int AttendanceId,DateTime CheckOut)
        {
          var attendance = await  _Context.AttendenceRecords.FindAsync(AttendanceId); 
            if(attendance != null)
            {
                attendance.CheckOut = CheckOut;
                await _Context.SaveChangesAsync();
            }
            return attendance;
        }

        public async Task<List<AttendenceRecord>> GetAttendancByEmployeeIdAsync(int employeeId)
        {
            var attendanceRecords = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId)
                .ToListAsync();
            return attendanceRecords;
        }
       public async Task<List<AttendenceRecord>> GetAttendanceByIdAndDate(int employeeId, DateTime date)
        {
            var attendanceRecords = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckIn.Date == date.Date)
                .ToListAsync();
            return attendanceRecords;
        }

        public async Task<List<DateTime>> GetAttendanceByIdAndMonth(int employeeId, int month, int year)
        {
            var presentDates = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckIn.Month == month && n.CheckIn.Year == year)
                .Select(n => n.CheckIn.Date)
                .Distinct()
                .ToListAsync();
            return presentDates;
        }

        public async Task<List<DateTime>> GetAbsentDates(int employeeId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            var absentDates = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckIn >= startDate && n.CheckIn < endDate)
                .Select(n => n.CheckIn.Date)
                .Distinct()
                .ToListAsync();
            return absentDates;

        }

        public async Task<List<DateTime>> GetPresentDates(int employeeId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            var presentDates = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckIn >= startDate && n.CheckIn < endDate)
                .Select(n => n.CheckIn.Date)
                .Distinct()
                .ToListAsync();
            return presentDates;
        }

       public async Task<List<AttendenceRecord>> GetCheckInByEmployeeAndMonth(int employeeId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            var checkInRecords = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckIn>=startDate&&n.CheckIn<endDate)
                .ToListAsync();
            return checkInRecords;
        }
        public async Task<List<AttendenceRecord>> GetCheckOutByEmployeeAndMonth(int employeeId, int month, int year)

        {

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            var checkOutRecords = await _Context.AttendenceRecords
                .Where(n => n.EmployeeId == employeeId && n.CheckOut.HasValue && n.CheckOut.Value >= startDate && n.CheckOut.Value < endDate)
                .ToListAsync();
            return checkOutRecords;
        }










    }
}
