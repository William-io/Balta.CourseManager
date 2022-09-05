using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Balta.CourseManager.App.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(LoginRequest loginRequest, IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        //Validação, verifico se o usuario faz parte do bando de dados. 
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();

        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();


        #region token e sua descrição p/ ser gerado
        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
            }),
            //Assinatura
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"], 
            Issuer = configuration["JwtBearerTokenSettings:Issuer"] 
        };
        #endregion

        #region  Criar o token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor); 

        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
        #endregion
    }
}