namespace Backend;

public class Group
{
    public Guid Id { get; set; }

    public List<Course> Courses { get; set; } = new List<Course>();

    public List<User> Members { get; set; } = new List<User>();
}
