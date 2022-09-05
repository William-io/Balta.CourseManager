using System.Linq;
using Balta.CourseManager.App.Infrastructure;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CourseGetAll
{
    public static string Template => "/courses";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(DataContext context)
    {
        var courses = context.Courses.ToList();

        var response = courses.Select(c => new CourseResponse
        (
            c.Id,
            c.Title,
            c.Summary,
            c.Tag,
            c.DurationInMinutes,
            c.Availability
        ));

        return Results.Ok(courses);
    }
}
