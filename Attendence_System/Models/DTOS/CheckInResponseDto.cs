namespace Attendence_System.Models.DTOS
{
    public class CheckInResponseDto 
    {
        public bool IsSuccess { get; set; } = true; // mandatory field to indicate if the operation was successful or not
        public AttendanceRecordDto? Data { get; set; } // optional field to hold the data of the attendance record if the operation was successful
        public string? ErrorMessage { get; set; } = null; // optional field to hold the error message if the operation was not successful
    }
}
