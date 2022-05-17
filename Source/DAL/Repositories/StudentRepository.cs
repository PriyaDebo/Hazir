using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class StudentRepository
    {
        private CosmosClient? client;
        private Database? database;
        private Container? container;
        readonly ConnectDatabase cd;

        public StudentRepository()
        {
            cd = new ConnectDatabase();
            client = cd.Client;
            database = client.GetDatabase("Hazir");
            container = database.GetContainer("Students");
        }

        public async Task<IEnumerable<IStudent>> GetAllStudentsAsync()
        {
            var students = new List<IStudent>();
            var query = "SELECT * FROM c";
            QueryDefinition definition = new QueryDefinition(query);
            var Iterator = container.GetItemQueryIterator<StudentData>(definition);
            while (Iterator.HasMoreResults)
            {
                var response = await Iterator.ReadNextAsync();
                foreach (var studentData in response)
                {
                    if (studentData != null)
                    {
                        students.Add(new Student()
                        {
                            Id = studentData.Id,
                            Name = studentData.Name,
                            JoinDate = studentData.JoinDate,
                            PersistedFaceId = studentData.PersistedFaceId,
                        });
                    }
                }
            }
            return students;
        }

        public async Task<IStudent> GetStudentByIdAsync(string id)
        {
            PartitionKey partitionKey = new PartitionKey(id);
            var response = await container.ReadItemAsync<StudentData>(id, partitionKey);
            if (response == null)
            {
                return null;
            }

            var studentResponse = new Student()
            {
                Id = response.Resource.Id,
                Name = response.Resource.Name,
                JoinDate = response.Resource.JoinDate,
                PersistedFaceId = response.Resource.PersistedFaceId
            };

            return studentResponse;
        }
    }
}
