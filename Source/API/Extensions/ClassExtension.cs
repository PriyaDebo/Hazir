using API.Models;
using Common.Models;

namespace API.Extensions
{
    public static class ClassExtension
    {
        public static ClassResponseModel ToAPIModel(this IClass iClass)
        {
            var model = new ClassResponseModel
            {
                Id = iClass.Id,
                TeacherId = iClass.TeacherId,
                StudentIds = iClass.StudentIds,
                CourseId = iClass.CourseId
            };

            if (model.Students == null)
            {
                model.Students = new List<StudentResponseModel>();
            }

            foreach (var student in iClass.Students)
            {
                model.Students.Add(student.ToAPIModel());
            }

            return model;
        }
    }
}
