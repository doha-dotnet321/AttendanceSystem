namespace Attendence_System.Models.DTOS
{
    public class PresentResponseDto
    {
        
            public List<DateTime>? Data { get; set; }
            public bool IsSuccess { get; set; } = false;
            public string? ErrorMassege { get; set; }
        
    }
}
