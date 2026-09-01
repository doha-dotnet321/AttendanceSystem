namespace Attendence_System.Models.DTOS
{
    public class  UpdateEmployeeResponse 
    {
        public bool IsSuccess { get; set; } = true;
        public UpdateEmployeeDto Data { get; set; }
        public string? ErrorMessage { get; set; } = null;

    }

}
