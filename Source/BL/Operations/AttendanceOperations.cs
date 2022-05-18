using Common.DTO;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class AttendanceOperations
    {
        AttendanceRepository attendanceRepository;
        StudentRepository studentRepository;

        public AttendanceOperations()
        {
            attendanceRepository = new AttendanceRepository();
            studentRepository = new StudentRepository();
        }

        public async Task<IAttendance> ToDTOModelAsync(IAttendance attendanceResponse)
        {
            var attendance = new Attendance()
            {
                Id = attendanceResponse.Id,
                Date = attendanceResponse.Date,
                ClassId = attendanceResponse.ClassId,
                PresentStudentIds = attendanceResponse.PresentStudentIds,
            };

            if (attendance.PresentStudents == null)
            {
                attendance.PresentStudents = new List<IStudent>();
            }

            foreach (var studentId in attendanceResponse.PresentStudentIds)
            {
                attendance.PresentStudents.Add(await studentRepository.GetStudentByIdAsync(studentId));
            }

            return attendance;
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

        public async Task<IAttendance> GetAttendanceByIdAsync(string id, string classId)
        {
            var attendanceResponse = await attendanceRepository.GetAttendanceDataAsync(id, classId);
            return await ToDTOModelAsync(attendanceResponse);
        }

        public async Task<IAttendance> GetAttendanceByClassAndDateAsync(string classId, string date)
        {
            var attendanceResponse = await attendanceRepository.GetAttendanceDataByClassAndDateAsync(classId, date);
            return await ToDTOModelAsync(attendanceResponse);
        }

        public async Task<bool> MarkAttendanceAsync(string id, string classId, string studentId)
        {
            return await attendanceRepository.MarkAttendanceAsync(id, classId, studentId);

        }

        public async Task<bool> UnmarkAttendanceAsync(string id, string classId, string studentId)
        {
            return await attendanceRepository.UnmarkAttendanceAsync(id, classId, studentId);
        }

    }
}
