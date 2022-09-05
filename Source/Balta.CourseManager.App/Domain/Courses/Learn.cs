namespace Balta.CourseManager.App.Domain.Courses;

public class Learn : Entity
{
    public Learn(string title, string description, string url, Guid Id)
    {
        Title = title;
        Description = description;
        Url = url;

        Id = Guid.NewGuid();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
}