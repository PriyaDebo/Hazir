using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    internal class StudentData : IStudent
    {
        public StudentData()
        {
        }

        public StudentData(StudentData student)
        {
            this.Id = student.Id;
            this.Name = student.Name;
            this.JoinDate = student.JoinDate;
            this.PersistedFaceId = student.PersistedFaceId;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "joinDate")]
        public string JoinDate { get; set; }

        [JsonProperty(PropertyName = "persistedFaceId")]
        public string PersistedFaceId { get; set; }
    }
}
