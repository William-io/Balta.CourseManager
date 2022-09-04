namespace Balta.CourseManager.App.Endpoints.Courses;

public class CourseResponse
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Tag { get; set; }
    public double DurationInMinutes { get; set; }
    public bool Availability { get; set; }
}
