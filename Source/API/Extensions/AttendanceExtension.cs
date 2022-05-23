using Common.APIModels;
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
            };

            if (attendance.PresentStudents == null)
            {
                return model;
            }

            if (model.PrsesentStudents == null)
            {
                model.PrsesentStudents = new List<StudentResponseModel>();
            }

            foreach (var student in attendance.PresentStudents)
            {
                model.PrsesentStudents.Add(student.ToAPIModel());
            }

            return model;
        }
    }

}
