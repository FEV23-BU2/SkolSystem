namespace Backend;

public class Course
{
    private static int ID_COUNTER = 0;

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Group> Groups { get; set; }

    public Course(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.Groups = new List<Group>();
        this.Id = ID_COUNTER++;
    }
}
