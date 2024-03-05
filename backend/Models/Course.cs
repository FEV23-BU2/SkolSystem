namespace Backend;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Group> Groups { get; set; }

    public Course() { }

    public Course(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.Groups = new List<Group>();
    }
}

public class MyFileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public string Extension { get; set; }

    public MyFileModel(string name, byte[] content, string extension)
    {
        this.Name = name;
        this.Content = content;
        this.Extension = extension;
    }
}
