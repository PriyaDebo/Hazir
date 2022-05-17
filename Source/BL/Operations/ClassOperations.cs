using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class ClassOperations
    {
        ClassRepository classRepository;
        StudentOperations studentOperations;

        public ClassOperations()
        {
            classRepository = new ClassRepository();
            studentOperations = new StudentOperations();
        }

        public async Task<IEnumerable<IClass>> GetAsync()
        {
            var responseClass = await classRepository.GetAllClassesAsync();
            foreach (var responseClassItem in responseClass)
            {
                var studentIds = responseClassItem.StudentIds;
                foreach (var studentId in studentIds)
                {
                    responseClassItem.Students.Add(await studentOperations.GetByIdAsync(studentId));
                }
            }
            return responseClass;
        }

        public async Task<IClass> GetClassByIdAsync(string id)
        {
            var responseClass = await classRepository.GetClassByIdAsync(id);
            var studentIds = responseClass.StudentIds;
            foreach (var studentId in studentIds)
            {
                responseClass.Students.Add(await studentOperations.GetByIdAsync(studentId));
            }

            return responseClass;

        }
    }
}
