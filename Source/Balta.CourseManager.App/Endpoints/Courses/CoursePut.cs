using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CoursePut
{
    public static string Template => "/courses/{id:guid}";

    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CourseRequest courseRequest, DataContext context)
    {
        var course = context.Courses.Where(c => c.Id == id).FirstOrDefault();

        if (course == null)
            return Results.NotFound();

        if (!course.IsValid)
            return Results.ValidationProblem(course.Notifications.ConvertProblemReporting());

        context.SaveChanges();

        return Results.Ok();
    }
}