using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using AutoMapper;

namespace Attendence_System.Models.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendenceRecord, AttendanceRecordDto>().ReverseMap();
            CreateMap<AttendenceRecord, AttendanceRecordDto>()
    .ForMember(dest => dest.HoursWorked, opt => opt.MapFrom(src =>
        src.CheckOut.HasValue
            ? Math.Round((src.CheckOut.Value - src.CheckIn).TotalHours, 2)
            : 0))
    .ReverseMap();
        }

    }
}
