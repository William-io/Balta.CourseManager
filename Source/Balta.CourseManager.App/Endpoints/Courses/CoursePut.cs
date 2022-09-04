using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Balta.CourseManager.App.Endpoints.Courses;

public class CoursePut
{
    public static string Template => "/courses/{id}";

    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CourseRequest courseRequest, DataContext context)
    {
        var course = context.Courses.Where(c => c.Id == id).FirstOrDefault();
        course.Title = courseRequest.Title;
        course.Summary = courseRequest.Summary;
        course.Tag = courseRequest.Tag;
        course.DurationInMinutes = courseRequest.DurationInMinutes;
        course.Availability = courseRequest.Availability; //true or false

        context.SaveChanges();

        return Results.Ok();
    }
}
