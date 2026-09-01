namespace Attendence_System.Models.DTOS
{
    public class GetAttendanceByIdAndDateResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public List<AttendanceRecordDto>? Data { get; set; }
        public string? ErrorMessage { get; set; } = null;
    }
}
