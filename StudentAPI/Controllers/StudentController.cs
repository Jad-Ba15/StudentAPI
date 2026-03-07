using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentAPI.Models;
using StudentAPI.ServicesLayer;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IMemoryCache _cache;

        public StudentController(IStudentService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        // --------------------------------
        // Async + Caching
        // --------------------------------
        [HttpGet("major/{id}")]
        public async Task<IActionResult> GetStudentsByMajor(int id)
        {
            string cacheKey = $"major_{id}";

            if (!_cache.TryGetValue(cacheKey, out List<Student> students))
            {
                students = await _service.GetStudentsByMajorAsync(id);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, students, cacheOptions);
            }

            return Ok(students);
        }

        // --------------------------------
        // Async
        // --------------------------------
        [HttpGet("course/{id}")]
        public async Task<IActionResult> GetStudentsByCourse(int id)
        {
            var students = await _service.GetStudentsByCourseAsync(id);

            return Ok(students);
        }

        // --------------------------------
        // Async
        // --------------------------------
        [HttpGet("passed/{id}")]
        public async Task<IActionResult> GetPassedStudents(int id)
        {
            var students = await _service.GetPassedStudentsAsync(id);

            return Ok(students);
        }

        // --------------------------------
        // Parallelism
        // --------------------------------
        [HttpGet("parallel/{majorId}/{courseId}")]
        public async Task<IActionResult> GetParallelStudents(int majorId, int courseId)
        {
            var majorTask = _service.GetStudentsByMajorAsync(majorId);
            var courseTask = _service.GetStudentsByCourseAsync(courseId);

            await Task.WhenAll(majorTask, courseTask);

            return Ok(new
            {
                StudentsByMajor = majorTask.Result,
                StudentsByCourse = courseTask.Result
            });
        }

        // --------------------------------
        // Task + Continuation (Assignment)
        // --------------------------------
        [HttpGet("random-calculation")]
        public async Task<IActionResult> RandomCalculation()
        {
            var task = Task.Run(() =>
            {
                Random rnd = new Random();

                int a = rnd.Next(1, 100);
                int b = rnd.Next(1, 100);

                return a * b;
            });

            var continuation = task.ContinueWith(t =>
            {
                return $"Random result: {t.Result}";
            });

            var result = await continuation;

            return Ok(result);
        }
    }
}