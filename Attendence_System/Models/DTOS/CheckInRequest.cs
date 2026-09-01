using System.ComponentModel.DataAnnotations;

namespace Attendence_System.Models.DTOS
{
    public class CheckInRequest
    {
        [Required]
        public int EmployeeId { get; set; } 
        [Required]
        public string EmployeeName { get; set; }
    }
}
