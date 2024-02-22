using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class CreateCourseDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public CourseDto(Course course)
    {
        this.Id = course.Id;
        this.Name = course.Name;
        this.Description = course.Description;
    }
}

[ApiController]
[Route("api/course")]
public class CourseController : ControllerBase
{
    CourseService courseService;

    public CourseController(CourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpPost]
    public IActionResult CreateCourse([FromBody] CreateCourseDto dto)
    {
        Course course = courseService.CreateCourse(dto.Name, dto.Description);

        return Ok(new CourseDto(course));
    }

    [HttpGet]
    public List<CourseDto> GetAllCourses()
    {
        return courseService.GetAll().Select(course => new CourseDto(course)).ToList();
    }
}
