namespace Attendence_System.Models.DTOS
{
    public class AttendanceRecordDto
    {
           public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool IsActive { get; set; }
        public int HoursWorked { get; set; }
    }
}
