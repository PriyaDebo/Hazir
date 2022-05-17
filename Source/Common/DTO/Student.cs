using Common.Models;

namespace Common.DTO
{
    public class Student : IStudent
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string JoinDate { get; set; }

        public string PersistedFaceId { get; set; }
    }
}
