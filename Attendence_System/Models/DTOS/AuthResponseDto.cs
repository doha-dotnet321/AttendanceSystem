namespace Attendence_System.Models.DTOS
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; } 
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; } 
    }
}
