namespace Attendence_System.Models.DTOS
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? EmployeeId { get; set; }
        public string Role { get; set; } = "Employee";
    }
}
