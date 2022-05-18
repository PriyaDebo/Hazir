using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class AttendanceRepository
    {
        private CosmosClient? client;
        private Database? database;
        private Container? container;
        readonly ConnectDatabase cd;

        public AttendanceRepository()
        {
            cd = new ConnectDatabase();
            client = cd.Client;
            database = client.GetDatabase("Hazir");
            container = database.GetContainer("Attendance");
        }

        public async Task<AttendanceData> CreateAttendanceDataAsync(string date, string classId)
        {
            var attendance = new AttendanceData()
            {
                Id = Guid.NewGuid().ToString(),
                Date = date,
                ClassId = classId,
                PresentStudentIds = new List<string>()
            };
            var attendanceItemCreated = await container.CreateItemAsync<AttendanceData>(attendance);
            return attendanceItemCreated.Resource;
        }

        public async Task MarkAttendanceAsync(string id, string classId, string studentId)
        {
            var attendance = await container.ReadItemAsync<AttendanceData>(id, new PartitionKey(classId));
            var newAttendance = new AttendanceData(attendance.Resource);
            if (newAttendance.PresentStudentIds == null)
            {
                newAttendance.PresentStudentIds = new List<string>();
            }
            newAttendance.PresentStudentIds.Append(studentId);
            await container.ReplaceItemAsync<AttendanceData>(newAttendance, id);
        }

        public async Task<IAttendance> GetAttendanceDataAsync(string id, string classId)
        {
            var response = await container.ReadItemAsync<AttendanceData>(id, new PartitionKey(classId));
            if (response == null)
            {
                return null;
            }
            var attendance = new Attendance()
            {
                ClassId = response.Resource.ClassId,
                Date = response.Resource.Date,
                Id = response.Resource.Id,
                PresentStudentIds = response.Resource.PresentStudentIds,
            };
            return attendance;
        }

        public async Task UnmarkAttendanceAsync(string id, string classId, string studentId)
        {
            var attendance = await container.ReadItemAsync<AttendanceData>(id, new PartitionKey(classId));
            foreach (var presentStudent in attendance.Resource.PresentStudentIds)
            {
                if (presentStudent == studentId)
                {
                    attendance.Resource.PresentStudentIds.Remove(studentId);
                }
            }
            await container.ReplaceItemAsync<AttendanceData>(attendance, id);
        }

        //public override async Task<IAttendance> GetAttendanceDataAsync(string classId, string date)
        //{
        //    var query = "SELECT * FROM Attendance WHERE Attendance.classId = {classId}";
        //    QueryDefinition queryDefinition = new QueryDefinition(query);
        //    var attendanceResponse = this.container.GetItemQueryIterator<AttendanceData>(queryDefinition);
        //    var response = await attendanceResponse.ReadNextAsync();
        //    var attendance = new Attendance()
        //    {
        //        Id = response.Resource.FirstOrDefault().Id,
        //        ClassId = response.Resource.FirstOrDefault().ClassId,
        //        Date = response.Resource.FirstOrDefault().Date,
        //        PresentStudentIds = response.Resource.FirstOrDefault().PresentStudentIds,
        //    };

        //    return attendance;
        //}
    }
}
