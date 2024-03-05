using System.Data;
using Microsoft.AspNetCore.Authorization;
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
    ApplicationContext context;

    public CourseController(CourseService courseService, ApplicationContext context)
    {
        this.courseService = courseService;
        this.context = context;
    }

    [HttpPost]
    [Authorize("create_course")]
    public IActionResult CreateCourse([FromBody] CreateCourseDto dto)
    {
        try
        {
            Course course = courseService.CreateCourse(dto.Name, dto.Description);
            return Ok(new CourseDto(course));
        }
        catch (DuplicateNameException)
        {
            return Conflict($"A course with the name '{dto.Name}' already exists.");
        }
        catch (ArgumentException)
        {
            return BadRequest($"'Name' and 'Description' must not be null or empty.");
        }
    }

    [HttpGet]
    public List<CourseDto> GetAllCourses()
    {
        return courseService.GetAll().Select(course => new CourseDto(course)).ToList();
    }

    [HttpPost("file")]
    public IActionResult UploadFile([FromForm] IFormFile file)
    {
        // Vad man vanligtvis gör här är att validera filen:
        // 1. Kolla storleken
        // 2. Kolla formatet
        // 3. Kolla innehållet

        using (MemoryStream stream = new MemoryStream())
        {
            file.CopyTo(stream);
            byte[] content = stream.ToArray();

            MyFileModel model = new MyFileModel(file.FileName, content, file.ContentType);
            context.Files.Add(model);
            context.SaveChanges();

            return Ok();
        }
    }

    [HttpGet("file")]
    public IActionResult DownloadFile([FromQuery] string fileName)
    {
        MyFileModel? model = context.Files.Where(file => file.Name == fileName).First();

        return File(model.Content, model.Extension, model.Name);
    }
}
