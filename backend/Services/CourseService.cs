namespace Backend;

public class CourseService
{
    ApplicationContext context;

    public CourseService(ApplicationContext context)
    {
        this.context = context;
    }

    public Course CreateCourse(string name, string description)
    {
        // TODO: Prevent course name duplicates
        Course course = new Course(name, description);

        context.Courses.Add(course);
        context.SaveChanges();

        return course;
    }

    public List<Course> GetAll()
    {
        return context.Courses.ToList();
    }
}
