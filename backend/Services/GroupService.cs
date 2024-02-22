namespace Backend;

public class GroupService
{
    ApplicationContext context;

    public GroupService(ApplicationContext context)
    {
        this.context = context;
    }

    public Group CreateGroup()
    {
        Group group = new Group();

        context.Groups.Add(group);
        context.SaveChanges();

        return group;
    }

    public List<Group> GetAll()
    {
        return context.Groups.ToList();
    }

    public void AddGroupToCourse(Guid courseId, Guid groupId)
    {
        Course? course = context.Courses.Find(courseId);
        Group? group = context.Groups.Find(groupId);

        // TODO: Better error handling
        if (course == null || group == null)
        {
            throw new ArgumentException("");
        }

        course.Groups.Add(group);
        group.Courses.Add(course);

        context.SaveChanges();
    }
}
