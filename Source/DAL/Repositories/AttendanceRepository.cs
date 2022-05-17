using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    internal class AttendanceRepository
    {
        private CosmosClient? client;
        private Database? database;
        private Container? container;
        readonly ConnectDatabase cd;

        AttendanceRepository()
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
                Date = date,
                ClassId = classId,
                PresentStudentIds = null
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
    }
}
