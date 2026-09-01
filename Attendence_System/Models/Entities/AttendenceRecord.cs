using System.ComponentModel.DataAnnotations;

namespace Attendence_System.Models.Entities
{
    public class AttendenceRecord
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int EmployeeId { get; set; } // foriegn key
        [Required]
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut{ get; set; }

        public Employee Employee { get; set; } = null!;
            // navigation property
    }
}
