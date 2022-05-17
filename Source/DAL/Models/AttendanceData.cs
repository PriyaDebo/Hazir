using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    internal class AttendanceData : IAttendance
    {
        public AttendanceData()
        {
        }

        public AttendanceData(AttendanceData attendance)
        {
            Id = attendance.Id;
            ClassId = attendance.ClassId;
            Date = attendance.Date;
            PresentStudentIds = attendance.PresentStudentIds;
        }


        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "classId")]
        public string ClassId { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "students")]
        public List<string> PresentStudentIds { get; set; }

        [JsonIgnore]
        public List<IStudent> PresentStudents { get; set; }
    }
}
