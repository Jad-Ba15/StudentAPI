using StudentAPI.Models;
using StudentAPI.Repositories;
using System.Collections.Generic;

namespace StudentAPI.ServicesLayer
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public List<Student> GetStudentsByMajor(int majorId)
        {
            return _repo.GetStudentsByMajor(majorId);
        }

        public List<Student> GetStudentsByCourse(int courseId)
        {
            return _repo.GetStudentsByCourse(courseId);
        }

        public List<Student> GetPassedStudents(int courseId)
        {
            return _repo.GetPassedStudents(courseId);
        }
    }
}