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
        public async Task<ClassResponseModel> GetSingleClassAsync(string id)
        {
            var classResponse = await classOperations.GetClassByIdAsync(id);
            return classResponse.ToAPIModel();
        }

        [HttpGet]
        [Route("GetStudentById")]
        public async Task<StudentResponseModel> GetSingleStudentAsync(string id)
        {
            var student = await studentOperations.GetByIdAsync(id);
            return student.ToAPIModel();
        }
    }
}