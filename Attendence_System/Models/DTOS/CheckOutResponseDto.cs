namespace Attendence_System.Models.DTOS
{
    public class CheckOutResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public AttendanceRecordDto Data { get; set; }
        public string? ErrorMessage { get; set; } = null;

    }
}
