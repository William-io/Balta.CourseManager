using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CourseDelete
{
    public static string Template => "/courses/{id}";

    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, DataContext context)
    {
        var course = context.Courses.Where(c => c.Id == id).FirstOrDefault();

        context.Remove(course);
        context.SaveChanges();

        return Results.Ok();
    }
}
