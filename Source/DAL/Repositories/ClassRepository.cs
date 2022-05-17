using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class ClassRepository
    {
        private CosmosClient? client;
        private Database? database;
        private Container? container;
        readonly ConnectDatabase cd;

        public ClassRepository()
        {
            cd = new ConnectDatabase();
            client = cd.Client;
            database = client.GetDatabase("Hazir");
            container = database.GetContainer("Classes");
        }
        public async Task<IEnumerable<IClass>> GetAllClassesAsync()
        {
            var classes = new List<IClass>();
            var query = "SELECT * FROM c";
            QueryDefinition definition = new QueryDefinition(query);
            var Iterator = container.GetItemQueryIterator<ClassData>(definition);
            while (Iterator.HasMoreResults)
            {
                var response = await Iterator.ReadNextAsync();
                foreach (var classData in response)
                {
                    if (classData != null)
                    {
                        classes.Add(new Class()
                        {
                            Id = classData.Id,
                            CourseId = classData.CourseId,
                            TeacherId = classData.TeacherId,
                            StudentIds = classData.StudentIds,
                        });
                    }
                }
            }
            return classes;
        }

        public async Task<IClass> GetClassByIdAsync(string id)
        {
            PartitionKey partitionKey = new PartitionKey(id);
            var response = await container.ReadItemAsync<ClassData>(id, partitionKey);
            if (response == null)
            {
                return null;
            }

            var classResponse = new Class()
            {
                Id = response.Resource.Id,
                CourseId = response.Resource.CourseId,
                StudentIds = response.Resource.StudentIds,
                TeacherId = response.Resource.TeacherId,
            };
            return classResponse;
        }
    }
}
