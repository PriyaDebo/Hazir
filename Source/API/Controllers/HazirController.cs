using API.Extensions;
using API.Models;
using BL.Operations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Hazir")]
    public class HazirController : ControllerBase
    {

        private readonly ILogger<HazirController> logger;
        StudentOperations studentOperations;
        ClassOperations classOperations;

        public HazirController(ILogger<HazirController> logger)
        {
            this.logger = logger;
            studentOperations = new StudentOperations();
            classOperations = new ClassOperations();
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IEnumerable<StudentResponseModel>> GetAllStudentsAsync()
        {
            var students = await studentOperations.GetAsync();
            var studentAPI = new List<StudentResponseModel>();
            foreach (var student in students)
            {
                studentAPI.Add(student.ToAPIModel());
            }
            return studentAPI;
        }

        [HttpGet]
        [Route("GetAllClasses")]
        public async Task<IEnumerable<ClassResponseModel>> GetAllClassesAsync()
        {
            var classes = await classOperations.GetAsync();
            var classAPI = new List<ClassResponseModel>();
            foreach (var singleClass in classes)
            {
                classAPI.Add(singleClass.ToAPIModel());
            }
            return classAPI;
        }

        [HttpGet]
        [Route("GetClassById")]
        public async Task<ClassResponseModel> GetSingleClassAsync()
        {
            var classResponse = await classOperations.GetClassByIdAsync("77f5bf43-5375-4218-8658-4dad24be3b9e");
            return classResponse.ToAPIModel();
        }

        [HttpGet]
        [Route("GetStudentById")]
        public async Task<StudentResponseModel> GetSingleStudentAsync()
        {
            var student = await studentOperations.GetByIdAsync("310daa64-6328-45c1-b24e-573e40431d94");
            return student.ToAPIModel();
        }
    }
}