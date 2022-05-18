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
        AttendanceOperations attendanceOperations;

        public HazirController(ILogger<HazirController> logger)
        {
            this.logger = logger;
            studentOperations = new StudentOperations();
            classOperations = new ClassOperations();
            attendanceOperations = new AttendanceOperations();
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
        [Route("GetClassById/class/{classId}")]
        public async Task<ClassResponseModel> GetSingleClassAsync(string id)
        {
            var classResponse = await classOperations.GetClassByIdAsync(id);
            return classResponse.ToAPIModel();
        }

        [HttpGet]
        [Route("GetStudentById/student/{studentId}")]
        public async Task<StudentResponseModel> GetSingleStudentAsync(string id)
        {
            var student = await studentOperations.GetByIdAsync(id);
            return student.ToAPIModel();
        }

        [HttpPut]
        [Route("CreateAttendanceItem/class/{classId}/date/{date}")]
        public async Task<ActionResult<AttendanceResponseModel>> CreateAttendanceItem(string classId, string date)
        {
            var isDate = DateOnly.TryParse(date, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date");
            }
            var attendance = await attendanceOperations.CreateAttendanceItem(parsedDate.ToString(), classId);
            return Ok(attendance.ToAPIModel());
        }

        [HttpGet]
        [Route("GetAttendanceById/attendance/{AttendanceId}/class/{classId}")]
        public async Task<ActionResult<AttendanceResponseModel>> GetByAttendanceId(string attendanceId, string classId)
        {
            var responseAttendance = await attendanceOperations.GetAttendanceByIdAsync(attendanceId, classId);
            return Ok(responseAttendance.ToAPIModel());
        }

        [HttpGet]
        [Route("GetAttendanceByClassAndDate/class/{classId}/date/{date}")]
        public async Task<ActionResult<AttendanceResponseModel>> GetAttendanceByClassAndDate(string classId, string date)
        {
            var isDate = DateOnly.TryParse(date, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date");
            }

            var responseAttendance = await attendanceOperations.GetAttendanceByClassAndDateAsync(classId, date);
            return Ok(responseAttendance.ToAPIModel());
        }

        [HttpPost]
        [Route("MarkAttendance/attendance/{attendanceId}/class/{classId}/student/{studentId}")]
        public async Task<ActionResult> MarkAttendance(string id, string classId, string studentId)
        {
            var response = await attendanceOperations.MarkAttendanceAsync(id, classId, studentId);
            if (response)
            {
                return Ok(response);
            }
            return NotFound("Invalid Id");
        }

        [HttpPost]
        [Route("UnmarkAttendance/attendance/{attendanceId}/class/{classId}/student/{studentId}")]
        public async Task<ActionResult> UnmarkAttendance(string id, string classId, string studentId)
        {
            var response = await attendanceOperations.UnmarkAttendanceAsync(id, classId, studentId);
            if (response)
            {
                return Ok(response);
            }
            return NotFound("Invalid Id");
        }
    }
}