using System.Data;

namespace Backend;

public class CourseService
{
    ApplicationContext context;
    ICourseRepository courseRepository;

    public CourseService(ApplicationContext context, ICourseRepository courseRepository)
    {
        this.context = context;
        this.courseRepository = courseRepository;
    }

    public Course CreateCourse(string name, string description)
    {
        int existingCount = context.Courses.Where(existing => existing.Name == name).Count();
        if (existingCount > 0)
        {
            throw new DuplicateNameException();
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException();
        }

        return courseRepository.SaveCourse(new Course(name, description));
    }

    public List<Course> GetAll()
    {
        return context.Courses.ToList();
    }
}
