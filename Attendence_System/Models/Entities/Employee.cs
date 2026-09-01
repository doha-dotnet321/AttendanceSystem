using System.ComponentModel.DataAnnotations;

namespace Attendence_System.Models.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<AttendenceRecord> AttendenceRecords { get; set; } =new List<AttendenceRecord>();

        public bool IsDeleted { get; set; } = false;

    }
}
