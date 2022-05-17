using Common.Models;
using DAL.Repositories;
using System.Diagnostics;

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
                if (responseClassItem.Students == null)
                {
                    responseClassItem.Students = new List<IStudent>();
                }

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
            if (responseClass.Students == null)
            {
                responseClass.Students = new List<IStudent>();
            }

            foreach (var studentId in studentIds)
            {
                Debug.WriteLine($"Student ID: {studentId}");
                responseClass.Students.Add(await studentOperations.GetByIdAsync(studentId));
            }

            return responseClass;
        }
    }
}
