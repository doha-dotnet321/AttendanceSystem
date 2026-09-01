
using Microsoft.EntityFrameworkCore;

namespace Attendence_System.Models.DTOS
{
    public class AbsentResponseDto
    {
        public List<DateTime>? Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
    
  
    }
