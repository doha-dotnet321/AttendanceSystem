using Attendence_System.Models;
using Attendence_System.Models.DTOS;
using Attendence_System.Models.Entities;
using Attendence_System.Repositories;
using AutoMapper;
using Azure;


namespace Attendence_System.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        //public Task<AttendanceRecordDto> CheckAttendanceExist(int employeeId, DateTime today)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<CheckInResponseDto> CheckInAsync(int employeeId)
        {
            var response = new CheckInResponseDto();
            // Check if the employee exists
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found";
                return response;
            }

            var today = DateTime.Now;
            var checkInRecord = await _attendanceRepository.CheckAttendanceExist(employeeId, today); // Check if the employee has already checked in today
            if (checkInRecord != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee has already checked in today";
                return response;
            }
            var checkinRecord = new AttendenceRecord
            {
                EmployeeId = employeeId,
                CheckIn = today
            };
            // Perform check-in
            var attendanceRecord = await _attendanceRepository.CheckInAsync(checkinRecord);    // use the repository(checkIn) to save the check-in record
            if (attendanceRecord != null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<AttendanceRecordDto>(attendanceRecord);
            }
            return response;
            // service layer is responsible for handling the business logic and coordinating the interaction between the controller and the repository.
            // It ensures that the check-in process is executed correctly,
            // including validation and mapping of data.
        }

        public async Task<CheckOutResponseDto> CheckOutAsync(int employeeId)
        {
            var response = new CheckOutResponseDto();
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found";
                return response;
            }
            var today = DateTime.Now;
            var OpenAttendanceRecord = await _attendanceRepository.CheckAttendanceExist(employeeId, today);
            if (OpenAttendanceRecord == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "No open attendance record found for today";
                return response;
            }
            if (OpenAttendanceRecord.CheckOut != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee has already checked out today";
                return response;
            }
            if (OpenAttendanceRecord.CheckOut < OpenAttendanceRecord.CheckIn)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Check-out time cannot be earlier than check-in time";
                return response;
            }
            var attendanceRecord = await _attendanceRepository.CheckOutAsync(OpenAttendanceRecord.id, today);    // use the repository(checkIn) to save the check-in record
            if (attendanceRecord != null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<AttendanceRecordDto>(attendanceRecord);
            }
            return response;


        }

        public async Task<GetAttendancByEmployeeIdRsponseDto> GetAttendancByEmployeeIdAsync(int employeeId)
        {
            var attendanceRecords = await _attendanceRepository.GetAttendancByEmployeeIdAsync(employeeId);
            var response = new GetAttendancByEmployeeIdRsponseDto();
            if (attendanceRecords == null || attendanceRecords.Count == 0)
            {
                response = new GetAttendancByEmployeeIdRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No attendance records found for the employee.",
                };
                return response;
            }

            var attendanceRecordsDto = _mapper.Map<List<AttendanceRecordDto>>(attendanceRecords);
            response.IsSuccess = true;
            response.Data = attendanceRecordsDto;
            return response;

        }

        public async Task<GetAttendanceByIdAndDateResponseDto> GetAttendanceByIdAndDate(int employeeId, DateTime date)
        {
            var attendanceRecords = await _attendanceRepository.GetAttendanceByIdAndDate(employeeId, date);
            var response = new GetAttendanceByIdAndDateResponseDto();
            if (attendanceRecords == null || attendanceRecords.Count == 0)
            {
                response = new GetAttendanceByIdAndDateResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No attendance records found for the employee on the specified date.",
                };
                return response;
            }
            var attendanceRecordsDto = _mapper.Map<List<AttendanceRecordDto>>(attendanceRecords);
            response.IsSuccess = true;
            response.Data = attendanceRecordsDto;
            return response;
        }

        public async Task<AbsentResponseDto> GetAbsentDates(int employeeId, int month, int year)
        {

            if (year == 0 || month == 0)
            {
                return new AbsentResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Month or year are required."

                };

            }
            if (month < 1 || month > 12)

                return new AbsentResponseDto
                { IsSuccess = false, ErrorMessage = "Month must be between 1 and 12." };

            var absentDates = await _attendanceRepository.GetAbsentDates(employeeId, month, year);

            // validate if absentDates is null or empty
            if (absentDates == null || absentDates.Count == 0)
            {

                return new AbsentResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No absent dates found for the employee in the specified month and year."


                };
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (absentDates.Count > daysInMonth)
            {
                return new AbsentResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = $"Number of absent days ({absentDates.Count}) exceeds the number of days in the specified month ({daysInMonth})."
                };
            }
            var absentDatesDto = new AbsentResponseDto
            {
                Data = absentDates,
                IsSuccess = true,
                ErrorMessage = null
            };
            return absentDatesDto;
        }


        public async Task<PresentResponseDto> GetPresentDates(int employeeId, int month, int year)
        {
            if (month < 1 || month > 12 || year < 1)
            {
                return new PresentResponseDto
                {
                    IsSuccess = false,
                    ErrorMassege = "Month or year are invalid."
                };
            }

            var presentDates = await _attendanceRepository.GetPresentDates(employeeId, month, year);

            if (presentDates == null || presentDates.Count == 0)
            {
                return new PresentResponseDto
                {
                    IsSuccess = false,
                    ErrorMassege = "No present dates found for the employee in the specified month and year."
                };
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (presentDates.Count > daysInMonth)
            {
                return new PresentResponseDto
                {
                    IsSuccess = false,
                    ErrorMassege = $"Number of present days ({presentDates.Count}) exceeds the number of days in the specified month ({daysInMonth})."
                };
            }

            return new PresentResponseDto
            {
                Data = presentDates,
                IsSuccess = true,
                ErrorMassege = null
            };
        }

        public async Task<GetAttendancByEmployeePerMonthRsponseDto> GetCheckInByEmployeeAndMonth(int employeeId, int month, int year)
        {
            if (employeeId <= 0)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Employee id must be a positive number."
                };
            }

            if (month < 1 || month > 12 || year < 1)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Month or year are invalid."
                };
            }

            var requestedDate = new DateTime(year, month, 1);
            if (requestedDate > DateTime.Today)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Cannot request check-in records for a future month."
                };
            }

            var records = await _attendanceRepository.GetCheckInByEmployeeAndMonth(employeeId, month, year);
            if (records == null || records.Count == 0)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No check-in records found for the employee in the specified month and year."
                };
            }

            return new GetAttendancByEmployeePerMonthRsponseDto
            {
                Data = _mapper.Map<List<AttendanceRecordDto>>(records),
                IsSuccess = true
            };
        }
        public async Task<GetAttendancByEmployeePerMonthRsponseDto> GetCheckOutByEmployeeAndMonth(int employeeId, int month, int year)
        {
            if (employeeId <= 0)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Employee id must be a positive number."
                };
            }
            if (month < 1 || month > 12 || year < 1)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Month or year are invalid."
                };
            }
            var requestedDate = new DateTime(year, month, 1);
            if (requestedDate > DateTime.Today)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Cannot request check-out records for a future month."
                };
            }
            var records = await _attendanceRepository.GetCheckOutByEmployeeAndMonth(employeeId, month, year);
            if (records == null || records.Count == 0)
            {
                return new GetAttendancByEmployeePerMonthRsponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No check-out records found for the employee in the specified month and year."
                };
            }
            return new GetAttendancByEmployeePerMonthRsponseDto
            {
                Data = _mapper.Map<List<AttendanceRecordDto>>(records),
                IsSuccess = true
            };


        }

    }
}
    

