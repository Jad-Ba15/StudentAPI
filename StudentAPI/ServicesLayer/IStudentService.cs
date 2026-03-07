using StudentAPI.Models;

namespace StudentAPI.ServicesLayer
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsByMajorAsync(int majorId);

        Task<List<Student>> GetStudentsByCourseAsync(int courseId);

        Task<List<Student>> GetPassedStudentsAsync(int courseId);
    }
}