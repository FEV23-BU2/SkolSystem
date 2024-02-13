using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class CreateCourseDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

[ApiController]
[Route("api/course")]
public class CourseController : ControllerBase
{
    private static List<Course> courses = new List<Course>();

    [HttpPost]
    public IActionResult CreateCourse([FromBody] CreateCourseDto dto)
    {
        Course course = new Course(dto.Name, dto.Description);
        courses.Add(course);
        return Ok(course);
    }

    [HttpGet]
    public List<Course> GetAllCourses()
    {
        return courses;
    }
}
