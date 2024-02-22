using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class CreateStudentDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateStudent([FromBody] CreateStudentDto dto)
    {
        // Student student = studentService.CreateStudent();
        return Ok();
    }
}
