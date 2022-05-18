using API.Models;
using Common.Models;

namespace API.Extensions
{
    public static class AttendanceExtension
    {
        public static AttendanceResponseModel ToAPIModel(this IAttendance attendance)
        {
            var model = new AttendanceResponseModel
            {
                Id = attendance.Id,
                ClassId = attendance.ClassId,
                Date = attendance.Date,
                PresentStudentIds = attendance.PresentStudentIds,
            };

            foreach (var student in attendance.PresentStudents)
            {
                model.PrsesentStudents.Add(student.ToAPIModel());
            }

            return model;
        }
    }

}
