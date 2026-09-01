using Attendence_System.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Attendence_System.Models.DTOS
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }  
        
    }
}

