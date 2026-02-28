using StudentAPI.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace StudentAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connection;

        public StudentRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(_connection))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found or empty in configuration.");
            }
        }

        public List<Student> GetStudentsByMajor(int majorId)
        {
            using var connection = new SqlConnection(_connection);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to open SQL connection. Verify the connection string and that the SQL Server is reachable.", ex);
            }

            string sql = "SELECT * FROM Student WHERE MajorID = @majorId";

            return connection.Query<Student>(sql, new { majorId }).ToList();
        }

        public List<Student> GetStudentsByCourse(int courseId)
        {
            using var connection = new SqlConnection(_connection);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to open SQL connection. Verify the connection string and that the SQL Server is reachable.", ex);
            }

            string sql = @"
                SELECT s.*
                FROM Student s
                JOIN Enrollment e ON s.StudentID = e.StudentID
                WHERE e.CourseID = @courseId";

            return connection.Query<Student>(sql, new { courseId }).ToList();
        }

        public List<Student> GetPassedStudents(int courseId)
        {
            using var connection = new SqlConnection(_connection);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to open SQL connection. Verify the connection string and that the SQL Server is reachable.", ex);
            }

            string sql = @"
                SELECT s.*
                FROM Student s
                JOIN Enrollment e ON s.StudentID = e.StudentID
                WHERE e.CourseID = @courseId
                AND e.Grade >= 50";

            return connection.Query<Student>(sql, new { courseId }).ToList();
        }
    }
}