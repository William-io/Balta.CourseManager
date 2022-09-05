using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CoursePost
{
    public static string Template => "/courses";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(CourseRequest courseRequest, DataContext context)
    {
        // var module = context.Modules.FirstOrDefault(m => m.Id == courseRequest.ModuleId);
        var course = new Course(courseRequest.Title, courseRequest.Summary, courseRequest.Tag, courseRequest.DurationInMinutes);

        if (!course.IsValid)
            return Results.ValidationProblem(course.Notifications.ConvertProblemReporting());


        context.Courses.Add(course);
        context.SaveChanges();

        return Results.Created($"/courses/{course.Id}", course.Id);
    }
}
