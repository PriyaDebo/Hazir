namespace API.Models
{
    public class AttendanceResponseModel
    {
        public string Id { get; set; }

        public string ClassId { get; set; }

        public string Date { get; set; }

        public List<string> PresentStudentIds { get; set; }

        public List<StudentResponseModel> PrsesentStudents { get; set; }
    }
}
