using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<MyFileModel> Files { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public interface ICourseRepository
{
    Course SaveCourse(Course course);
}

public class CourseRepository : ICourseRepository
{
    ApplicationContext context;

    public CourseRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public Course SaveCourse(Course course)
    {
        context.Courses.Add(course);
        context.SaveChanges();
        return course;
    }
}
