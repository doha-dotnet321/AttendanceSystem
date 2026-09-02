using Microsoft.AspNetCore.Identity;

namespace Attendence_System.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
