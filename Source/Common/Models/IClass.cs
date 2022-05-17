namespace Common.Models
{
    public interface IClass
    {
        string Id { get; }

        string Name { get; }

        string CourseId { get; }

        string TeacherId { get; }

        List<string> StudentIds { get; }

        List<IStudent> Students { get; set; }
    }
}
