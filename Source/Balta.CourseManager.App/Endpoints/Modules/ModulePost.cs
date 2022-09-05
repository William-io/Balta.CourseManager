using Balta.CourseManager.App.Domain.Courses;
using Balta.CourseManager.App.Infrastructure;

namespace Balta.CourseManager.App.Endpoints.Modules;

public class ModulePost
{
    public static string Template => "/modules";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(ModuleRequest moduleRequest, DataContext context)
    {
        var adicionarModuloAoCurso = context.Courses.FirstOrDefault(c => c.Id == moduleRequest.CourseId);

        var module = new Module(moduleRequest.Title, moduleRequest.Description, adicionarModuloAoCurso, moduleRequest.Learns);

        if (!module.IsValid)
            return Results.ValidationProblem(module.Notifications.ConvertProblemReporting());

        context.Modules.Add(module);

        context.SaveChanges();

        return Results.Created($"/modules/{module.Id}", module.Id);
    }
}