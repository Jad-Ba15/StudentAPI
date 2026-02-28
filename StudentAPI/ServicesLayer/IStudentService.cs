using StudentAPI.Models;
using System.Collections.Generic;

namespace StudentAPI.ServicesLayer
{
    public interface IStudentService
    {
        List<Student> GetStudentsByMajor(int majorId);
        List<Student> GetStudentsByCourse(int courseId);
        List<Student> GetPassedStudents(int courseId);
    }
}