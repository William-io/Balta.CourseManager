using Balta.CourseManager.App.Endpoints.Modules;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Balta.CourseManager.App.Endpoints;

public class ShowCase
{
    public static string Template => "/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(DataContext context, int? page, int? row, string? orderBy)
    {
        if (page == null)
            page = 1;

        if (row == null)
            row = 10;

        if (string.IsNullOrEmpty(orderBy))
            orderBy = "title";

        var query = context.Modules.Include(m => m.Course)
            .Where(m => m.Course.Availability);

        if (orderBy == "title")
            query = query.OrderBy(c => c.Title);

        context.Modules.Include(m => m.Learn).ToList();

        var results = query.Select(c => new ModuleResponse
        (
            c.Id,
            c.Course.Title,
            c.Title,
            c.Description,
            c.Learn.Title,
            c.Learn.Description,
            c.Learn.Url
        ));

        return Results.Ok(results);
    }
}