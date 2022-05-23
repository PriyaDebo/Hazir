namespace Common.APIModels
{
    public class ClassResponseModel
    {
        public string Id { get; set; }

        public string CourseId { get; set; }

        public string TeacherId { get; set; }

        public string Name { get; set; }

        public List<string> StudentIds { get; set; }

        public List<StudentResponseModel> Students { get; set; }
    }
}
