using Common.Models;


namespace Common.DTO
{
    public class Class : IClass
    {
        public string Id { get; set; }

        public string CourseId { get; set; }

        public string TeacherId { get; set; }

        public List<string> StudentIds { get; set; }

        public string Name { get; set; }

        public List<IStudent> Students { get; set; }
    }
}
