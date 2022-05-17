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
            this.Id = classData.Id;
            this.Name = classData.Name;
            this.StudentIds = classData.StudentIds;
            this.CourseId = classData.CourseId;
            this.TeacherId = classData.TeacherId;
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
