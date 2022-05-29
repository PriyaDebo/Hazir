using Common.Models;

namespace Common.DTO
{
    public class Teacher : ITeacher
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
