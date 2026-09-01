using System.ComponentModel.DataAnnotations;

namespace Attendence_System.Models.DTOS
{
    public class CheckOutRequest
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
