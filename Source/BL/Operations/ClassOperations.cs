using Common.Models;
using DAL.Repositories;
using System.Diagnostics;

namespace BL.Operations
{
    public class ClassOperations
    {
        ClassRepository classRepository;
        StudentOperations studentOperations;

        public ClassOperations(ClassRepository classRepository, StudentOperations studentOperations)
        {
            this.classRepository = classRepository;
            this.studentOperations = studentOperations;
        }

        public async Task<IEnumerable<IClass>> GetAsync()
        {
            var responseClass = await classRepository.GetAllClassesAsync();
            if (responseClass == null)
            {
                return null;
            }

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

        public async Task<IEnumerable<IClass>> GetClassByTeacher(string teacherId)
        {
            var responseClass = await classRepository.GetClassByTeacher(teacherId);
            if (responseClass == null)
            {
                return null;
            }

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
            if (responseClass == null)
            {
                return null;
            }

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
