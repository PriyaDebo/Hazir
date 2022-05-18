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
            Id = student.Id;
            Name = student.Name;
            JoinDate = student.JoinDate;
            PersistedFaceId = student.PersistedFaceId;
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
