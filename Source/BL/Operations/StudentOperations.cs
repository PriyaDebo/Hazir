using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class StudentOperations
    {
        StudentRepository students;

        public StudentOperations(StudentRepository studentRepository)
        {
            this.students = studentRepository;
        }

        public async Task<IEnumerable<IStudent>> GetAsync()
        {
            return await students.GetAllStudentsAsync();
        }

        public async Task<IStudent> GetByIdAsync(string id)
        {
            return await students.GetStudentByIdAsync(id);
        }

        public async Task<List<IStudent>> GetStudentsByIdAsync(List<string> ids)
        {
            var students = new List<IStudent>();
            foreach (var id in ids)
            {
                students.Add(await GetByIdAsync(id));
            }
            return students;
        }
    }
}
