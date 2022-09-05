using Balta.CourseManager.App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Balta.CourseManager.App.Endpoints.Modules;


public class ModuleGetAll
{
    public static string Template => "/modules";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(DataContext context)
    {
        var modules = context.Modules.Include(m => m.Course).OrderBy(m => m.Title).ToList();

        var response = modules.Select(c => new ModuleResponse
        (
            c.Id,
            c.Title,
            c.Course.Title,
            c.Description,
            c.Learn.Title,
            c.Learn.Description,
            c.Learn.Url
        ));

        return Results.Ok(modules);
    }
}
