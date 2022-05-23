using Common.APIModels;
using Common.Models;

namespace API.Extensions
{
    public static class StudentExtensions
    {
        public static StudentResponseModel ToAPIModel(this IStudent student)
        {
            var model = new StudentResponseModel
            {
                Id = student.Id,
                Name = student.Name,
                JoinDate = student.JoinDate,
                PersistedFaceid = student.PersistedFaceId
            };
            return model;
        }
    }
}
