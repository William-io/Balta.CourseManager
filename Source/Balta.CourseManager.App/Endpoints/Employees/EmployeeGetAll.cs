using Microsoft.AspNetCore.Identity;

namespace Balta.CourseManager.App.Employess;

public class EmployeeGetAll
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    {
        ///employees?page=1&rows=5 

        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();

        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;

            var claimName = claims.FirstOrDefault(c => c.Type == "Name");

            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }

        return Results.Ok(employees);
    }
}