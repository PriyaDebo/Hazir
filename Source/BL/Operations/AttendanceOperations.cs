using Common.DTO;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class AttendanceOperations
    {
        AttendanceRepository attendanceRepository;

        public AttendanceOperations()
        {
            attendanceRepository = new AttendanceRepository();
        }

        public async Task<IAttendance> CreateAttendanceItem(string date, string classId)
        {
            var response = await attendanceRepository.CreateAttendanceDataAsync(date, classId);
            var attendance = new Attendance()
            {
                ClassId = response.ClassId,
                Date = response.Date,
                Id = response.Id,
                PresentStudentIds = response.PresentStudentIds,
            };

            return attendance;
        }


    }
}
