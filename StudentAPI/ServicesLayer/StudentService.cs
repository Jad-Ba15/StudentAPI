using StudentAPI.Models;
using StudentAPI.Repositories;

namespace StudentAPI.ServicesLayer
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> GetStudentsByMajorAsync(int majorId)
        {
            return await Task.Run(() => _repo.GetStudentsByMajor(majorId));
        }

        public async Task<List<Student>> GetStudentsByCourseAsync(int courseId)
        {
            return await Task.Run(() => _repo.GetStudentsByCourse(courseId));
        }

        public async Task<List<Student>> GetPassedStudentsAsync(int courseId)
        {
            return await Task.Run(() => _repo.GetPassedStudents(courseId));
        }
    }
}