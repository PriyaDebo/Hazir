using Common.Models;

namespace Common.DTO
{
    public class Attendance : IAttendance
    {
        public string Id { get; set; }

        public string ClassId { get; set; }

        public string Date { get; set; }

        public List<string> PresentStudentIds { get; set; }

        public List<IStudent> PresentStudents { get; set; }
    }
}