using Balta.CourseManager.App.Domain.Courses;

namespace Balta.CourseManager.App.Endpoints.Courses;

public record CourseResponse(Guid Id, string Title, string Summary, string Tag, double DurationInMinutes, bool Availability);
