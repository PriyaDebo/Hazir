using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ClassData : IClass
    {
        public ClassData()
        {
        }

        public ClassData(ClassData classData)
        {
            Id = classData.Id;
            Name = classData.Name;
            StudentIds = classData.StudentIds;
            CourseId = classData.CourseId;
            TeacherId = classData.TeacherId;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "courseId")]
        public string CourseId { get; set; }

        [JsonProperty(PropertyName = "teacherId")]
        public string TeacherId { get; set; }

        [JsonProperty(PropertyName = "students")]
        public List<string> StudentIds { get; set; }

        [JsonIgnore]
        public List<IStudent> Students { get; set; }
    }
}
