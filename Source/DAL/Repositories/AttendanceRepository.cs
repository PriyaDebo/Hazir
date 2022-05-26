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

        public async Task<IAttendance> GetAttendanceDataAsync(string id, string classId)
        {
            var partitionKey = new PartitionKey(classId);
            var response = await container.ReadItemAsync<AttendanceData>(id, partitionKey);
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

        public async Task<IAttendance> GetAttendanceDataByClassAndDateAsync(string classId, string date)
        {
            var query = $"SELECT * FROM Attendance WHERE Attendance.classId = @classId AND Attendance.date = @date";
            QueryDefinition queryDefinition = new QueryDefinition(query).WithParameter("@classId", classId).WithParameter("@date", date);
            var attendanceResponse = container.GetItemQueryIterator<AttendanceData>(queryDefinition);
            var response = await attendanceResponse.ReadNextAsync();
            if (response.Resource.FirstOrDefault() == null)
            {
                return null;
            }

            var attendance = new Attendance()
            {
                Id = response.Resource.FirstOrDefault().Id,
                ClassId = response.Resource.FirstOrDefault().ClassId,
                Date = response.Resource.FirstOrDefault().Date,
                PresentStudentIds = response.Resource.FirstOrDefault().PresentStudentIds,
            };

            return attendance;
        }

        public async Task<bool> MarkAttendanceAsync(string id, string classId, string studentId)
        {
            var attendance = await container.ReadItemAsync<AttendanceData>(id, new PartitionKey(classId));
            if (attendance == null)
            {
                return false;
            }
            var newAttendance = new AttendanceData(attendance.Resource);
            if (newAttendance.PresentStudentIds == null)
            {
                newAttendance.PresentStudentIds = new List<string>();
            }
            newAttendance.PresentStudentIds.Add(studentId);
            await container.ReplaceItemAsync<AttendanceData>(newAttendance, id);
            return true;
        }

        public async Task<bool> UnmarkAttendanceAsync(string id, string classId, string studentId)
        {
            var attendance = await container.ReadItemAsync<AttendanceData>(id, new PartitionKey(classId));
            if (attendance == null)
            {
                return false;
            }

            var newAttendance = new AttendanceData()
            {
                Id = attendance.Resource.Id,
                ClassId = attendance.Resource.ClassId,
                Date = attendance.Resource.Date,
            };

            newAttendance.PresentStudentIds = new List<string>();
            foreach (var presentStudent in attendance.Resource.PresentStudentIds)
            {
                if (presentStudent != studentId)
                {
                    newAttendance.PresentStudentIds.Remove(studentId);
                }
            }

            await container.ReplaceItemAsync<AttendanceData>(newAttendance, id);
            return true;
        }
    }
}
