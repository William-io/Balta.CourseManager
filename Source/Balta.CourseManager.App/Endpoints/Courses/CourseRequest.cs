namespace Balta.CourseManager.App.Endpoints.Courses
{
    public record CourseRequest(string Title, string Summary, string Tag, double DurationInMinutes, bool Availability);
}