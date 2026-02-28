using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetStudentsByMajor(int majorId);

        List<Student> GetStudentsByCourse(int courseId);

        List<Student> GetPassedStudents(int courseId);
    }
}