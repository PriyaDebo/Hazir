using API.Extensions;
using BL.Operations;
using Common.APIModels;
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
        public async Task<ActionResult<IEnumerable<ClassResponseModel>>> GetAllClassesAsync()
        {
            var classes = await classOperations.GetAsync();
            var classAPI = new List<ClassResponseModel>();
            foreach (var singleClass in classes)
            {
                classAPI.Add(singleClass.ToAPIModel());
            }
            return Ok(classAPI);
        }

        [HttpGet]
        [Route("GetClassesByTeacher/teachers/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassResponseModel>>> GetClassesByTeacherAsync(string teacherId)
        {
            var isTeacherId = Guid.TryParse(teacherId, out var parsedTeacherId);
            if (!isTeacherId)
            {
                return BadRequest("Invalid Teacher Id");
            }

            var classes = await classOperations.GetClassByTeacher(parsedTeacherId.ToString());
            var classAPI = new List<ClassResponseModel>();
            foreach (var singleClass in classes)
            {
                classAPI.Add(singleClass.ToAPIModel());
            }
            return Ok(classAPI);
        }

        [HttpGet]
        [Route("GetClassById/classes/{classId}")]
        public async Task<ActionResult<ClassResponseModel>> GetSingleClassAsync(string classId)
        {
            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var classResponse = await classOperations.GetClassByIdAsync(parsedClassId.ToString());
            return Ok(classResponse.ToAPIModel());
        }

        [HttpGet]
        [Route("GetStudentById/students/{studentId}")]
        public async Task<ActionResult<StudentResponseModel>> GetSingleStudentAsync(string studentId)
        {
            var isStudentId = Guid.TryParse(studentId, out var parsedStudentId);
            if (!isStudentId)
            {
                return BadRequest("Invalid Student Id");
            }

            var student = await studentOperations.GetByIdAsync(parsedStudentId.ToString());
            return student.ToAPIModel();
        }

        [HttpPut]
        [Route("CreateAttendanceItem/classes/{classId}/date/{date}")]
        public async Task<ActionResult<AttendanceResponseModel>> CreateAttendanceItem(string classId, string date)
        {
            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var isDate = DateOnly.TryParse(date, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date");
            }
            var attendance = await attendanceOperations.CreateAttendanceItem(parsedDate.ToString(), parsedClassId.ToString());
            return Ok(attendance.ToAPIModel());
        }

        [HttpGet]
        [Route("GetAttendanceById/attendance/{attendanceId}/classes/{classId}")]
        public async Task<ActionResult<AttendanceResponseModel>> GetByAttendanceId(string attendanceId, string classId)
        {
            var isAttendanceId = Guid.TryParse(attendanceId, out var parsedAttendanceId);
            if (!isAttendanceId)
            {
                return BadRequest("Invalid Attendance Id");
            }

            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var responseAttendance = await attendanceOperations.GetAttendanceByIdAsync(parsedAttendanceId.ToString(), parsedClassId.ToString());
            return Ok(responseAttendance.ToAPIModel());
        }

        [HttpGet]
        [Route("GetAttendanceByClassAndDate/classes/{classId}/date/{date}")]
        public async Task<ActionResult<AttendanceResponseModel>> GetAttendanceByClassAndDate(string classId, string date)
        {
            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var isDate = DateOnly.TryParse(date, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date");
            }

            var responseAttendance = await attendanceOperations.GetAttendanceByClassAndDateAsync(parsedClassId.ToString(), parsedDate.ToString());
            return Ok(responseAttendance.ToAPIModel());
        }

        [HttpPost]
        [Route("MarkAttendance/attendance/{attendanceId}/classes/{classId}/students/{studentId}")]
        public async Task<ActionResult> MarkAttendance(string attendanceId, string classId, string studentId)
        {
            var isAttendanceId = Guid.TryParse(attendanceId, out var parsedAttendanceId);
            if (!isAttendanceId)
            {
                return BadRequest("Invalid Attendance Id");
            }

            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var isStudentId = Guid.TryParse(studentId, out var parsedStudentId);
            if (!isStudentId)
            {
                return BadRequest("Invalid Student Id");
            }

            var response = await attendanceOperations.MarkAttendanceAsync(parsedAttendanceId.ToString(), parsedClassId.ToString(), parsedStudentId.ToString());
            if (response)
            {
                return Ok(response);
            }
            return NotFound("Invalid Id");
        }

        [HttpPost]
        [Route("UnmarkAttendance/attendance/{attendanceId}/classes/{classId}/students/{studentId}")]
        public async Task<ActionResult> UnmarkAttendance(string attendanceId, string classId, string studentId)
        {
            var isAttendanceId = Guid.TryParse(attendanceId, out var parsedAttendanceId);
            if (!isAttendanceId)
            {
                return BadRequest("Invalid Attendance Id");
            }

            var isClassId = Guid.TryParse(classId, out var parsedClassId);
            if (!isClassId)
            {
                return BadRequest("Invalid Class Id");
            }

            var isStudentId = Guid.TryParse(studentId, out var parsedStudentId);
            if (!isStudentId)
            {
                return BadRequest("Invalid Student Id");
            }

            var response = await attendanceOperations.UnmarkAttendanceAsync(parsedAttendanceId.ToString(), parsedClassId.ToString(), parsedStudentId.ToString());
            if (response)
            {
                return Ok(response);
            }
            return NotFound("Invalid Id");
        }
    }
}