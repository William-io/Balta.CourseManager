using Balta.CourseManager.App.Domain.Courses;

namespace Balta.CourseManager.App.Endpoints.Modules
{
    public record ModuleResponse(
        Guid Id,
        string Course,
        string Module,
        string Description,
        string Learn,
        string DescriptionLearn,
        string Url);
}