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
        {
            //Retorna ID
            Title = c.Title,
            Summary = c.Summary,
            Tag = c.Tag,
            DurationInMinutes = c.DurationInMinutes,
            Availability = c.Availability
        });

        return Results.Ok(courses);
    }
}
