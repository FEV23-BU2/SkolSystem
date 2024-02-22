namespace Backend;

public class Group
{
    public Guid Id { get; set; }

    public List<Course> Courses { get; set; } = new List<Course>();

    public List<Student> Students { get; set; } = new List<Student>();
}
