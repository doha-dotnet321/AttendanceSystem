namespace Attendence_System.Models.DTOS
{
    public class DeleteEmployeeResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? ErrorMessage { get; set; } = null;
    }
}
