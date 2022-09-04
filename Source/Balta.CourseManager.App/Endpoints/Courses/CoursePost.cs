using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CoursePost
{
    public static string Template => "/courses";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(CourseRequest courseRequest, DataContext context)
    {
        var course = new Course
        {
            Title = courseRequest.Title,
            Summary = courseRequest.Summary,
            Tag = courseRequest.Tag,
            DurationInMinutes = courseRequest.DurationInMinutes
        };

        context.Courses.Add(course);
        context.SaveChanges();

        return Results.Created($"/courses/{course.Id}", course.Id);
    }
}
