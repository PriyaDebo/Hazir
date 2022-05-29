using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class ClassRepository
    {
        private CosmosClient client;
        private Database database;
        private Container container;

        public ClassRepository(CosmosClient client)
        {
            this.client = client;
            this.database = client.GetDatabase("Hazir");
            this.container = database.GetContainer("Classes");
        }

        public async Task<IEnumerable<IClass>> GetClassByTeacher(string teacherId)
        {
            var classes = new List<IClass>();
            var query = "SELECt * from Classes WHERE Classes.teacherId = @teacherId";
            var queryDefinition = new QueryDefinition(query).WithParameter("@teacherId", teacherId);
            var Iterator = container.GetItemQueryIterator<ClassData>(queryDefinition);
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
                            Name = classData.Name,
                            CourseId = classData.CourseId,
                            TeacherId = classData.TeacherId,
                            StudentIds = classData.StudentIds,
                        });
                    }
                }
            }

            return classes;
        }

        public async Task<IEnumerable<IClass>> GetAllClassesAsync()
        {
            var classes = new List<IClass>();
            var query = "SELECT * FROM c";
            var definition = new QueryDefinition(query);
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
                            Name = classData.Name,
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
            var partitionKey = new PartitionKey(id);
            var response = await container.ReadItemAsync<ClassData>(id, partitionKey);
            if (response == null)
            {
                return null;
            }

            var classResponse = new Class()
            {
                Id = response.Resource.Id,
                CourseId = response.Resource.CourseId,
                Name = response.Resource.Name,
                StudentIds = response.Resource.StudentIds,
                TeacherId = response.Resource.TeacherId,
            };

            return classResponse;
        }
    }
}
