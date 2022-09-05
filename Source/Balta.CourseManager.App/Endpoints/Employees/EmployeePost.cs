using System.Security.Claims;
using Balta.CourseManager.App.Endpoints;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Balta.CourseManager.App.Employess;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {

        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertProblemReporting());

        #region //Lista de claims de um usuario 
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeLicense", employeeRequest.EmployeeLicense),
            new Claim("Name", employeeRequest.Name)
        };

        //Adicionar lista de Claims
        var claim = userManager.AddClaimsAsync(user, userClaims).Result;
        #endregion

        if (!claim.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
}