namespace Common.Models
{
    public interface IAttendance
    {
        string Id { get; }

        string ClassId { get; }

        string Date { get; }

        List<string> PresentStudentIds { get; }

        List<IStudent> PresentStudents { get; }
    }
}
