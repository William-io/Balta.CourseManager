using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace Balta.CourseManager.App.Endpoints;

public static class ProblemReportingExtension
{
    public static Dictionary<string, string[]> ConvertProblemReporting(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertProblemReporting(this IEnumerable<IdentityError> error)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());
        return dictionary;
    }
}