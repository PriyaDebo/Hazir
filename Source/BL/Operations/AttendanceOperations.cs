using Common.DTO;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class AttendanceOperations
    {
        AttendanceRepository attendanceRepository;
        StudentRepository studentRepository;

        public AttendanceOperations(AttendanceRepository attendanceRepository, StudentRepository studentRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.studentRepository = studentRepository;
        }

        public async Task<IAttendance> AddStudentDataAsync(IAttendance attendanceResponse)
        {
            var attendance = new Attendance()
            {
                Id = attendanceResponse.Id,
                Date = attendanceResponse.Date,
                ClassId = attendanceResponse.ClassId,
                PresentStudentIds = attendanceResponse.PresentStudentIds,
            };

            if (attendance.PresentStudentIds != null)
            {
                if (attendance.PresentStudents == null)
                {
                    attendance.PresentStudents = new List<IStudent>();
                }


                foreach (var studentId in attendanceResponse.PresentStudentIds)
                {
                    var student = await studentRepository.GetStudentByIdAsync(studentId);
                    if (student != null)
                    {
                        attendance.PresentStudents.Add(await studentRepository.GetStudentByIdAsync(studentId));
                    }
                }
            }
            return attendance;
        }

        public async Task<IAttendance> CreateAttendanceItem(string date, string classId)
        {
            var alreadyExists = await attendanceRepository.GetAttendanceDataByClassAndDateAsync(classId, date);
            if (alreadyExists != null)
            {
                return await GetAttendanceByClassAndDateAsync(classId, date);
            }

            var response = await attendanceRepository.CreateAttendanceDataAsync(date, classId);
            var attendance = new Attendance()
            {
                ClassId = response.ClassId,
                Date = response.Date,
                Id = response.Id,
            };

            return attendance;
        }

        public async Task<IAttendance> GetAttendanceByIdAsync(string id, string classId)
        {
            var attendanceResponse = await attendanceRepository.GetAttendanceDataAsync(id, classId);
            return await AddStudentDataAsync(attendanceResponse);
        }

        public async Task<IAttendance?> GetAttendanceByClassAndDateAsync(string classId, string date)
        {
            var attendanceResponse = await attendanceRepository.GetAttendanceDataByClassAndDateAsync(classId, date);
            if (attendanceResponse != null)
            {
                return await AddStudentDataAsync(attendanceResponse);
            }

            return null;
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
