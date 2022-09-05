using System.Text;
using Balta.CourseManager.App.Employess;
using Balta.CourseManager.App.Endpoints;
using Balta.CourseManager.App.Endpoints.Courses;
// using Balta.CourseManager.App.Endpoints.Learns;
using Balta.CourseManager.App.Endpoints.Modules;
using Balta.CourseManager.App.Endpoints.Security;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<DataContext>(builder.Configuration["ConnectionString:CourseManagerDb"]);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{   //Diminuir nivel de segurança na criação de senha
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<DataContext>();

#region JWT, Config
// builder.Services.AddAuthorization();

builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddAuthentication(x =>
{
    //Vai se autenticar atraves do JWT
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //Validações
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();


#region endpoint: /courses
app.MapMethods(CoursePost.Template, CoursePost.Methods, CoursePost.Handle);
app.MapMethods(CourseGetAll.Template, CourseGetAll.Methods, CourseGetAll.Handle);
// app.MapMethods(CoursePut.Template, CoursePut.Methods, CoursePut.Handle);
// app.MapMethods(CourseDelete.Template, CourseDelete.Methods, CourseDelete.Handle);
#endregion

#region endpoint: /modules
app.MapMethods(ModulePost.Template, ModulePost.Methods, ModulePost.Handle);
// app.MapMethods(ModuleGetAll.Template, ModuleGetAll.Methods, ModuleGetAll.Handle);
#endregion

#region endpoint: /learns == aulas
// app.MapMethods(LearnPost.Template, LearnPost.Methods, LearnPost.Handle);
#endregion

#region endpoint: /employees 
app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);
#endregion

app.MapMethods(ShowCase.Template, ShowCase.Methods, ShowCase.Handle);

app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);

app.Run();
