using Balta.CourseManager.App.Domain.Courses;

namespace Balta.CourseManager.App.Endpoints.Modules;
public record ModuleRequest(string Title, string Description, Guid CourseId, Learn Learns);


