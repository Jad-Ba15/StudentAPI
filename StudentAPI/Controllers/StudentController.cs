using Microsoft.AspNetCore.Mvc;
using StudentAPI.ServicesLayer;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet("major/{id}")]
        public IActionResult GetStudentsByMajor(int id)
        {
            return Ok(_service.GetStudentsByMajor(id));
        }

        [HttpGet("course/{id}")]
        public IActionResult GetStudentsByCourse(int id)
        {
            return Ok(_service.GetStudentsByCourse(id));
        }

        [HttpGet("passed/{id}")]
        public IActionResult GetPassedStudents(int id)
        {
            return Ok(_service.GetPassedStudents(id));
        }
    }
}