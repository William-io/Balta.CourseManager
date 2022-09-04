using Balta.CourseManager.App.Endpoints.Courses;
using Balta.CourseManager.App.Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<DataContext>(builder.Configuration["ConnectionString:CourseManagerDb"]);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Acesso endpoint: Cursos
app.MapMethods(CoursePost.Template, CoursePost.Methods, CoursePost.Handle);
app.MapMethods(CourseGetAll.Template, CourseGetAll.Methods, CourseGetAll.Handle);
app.MapMethods(CoursePut.Template, CoursePut.Methods, CoursePut.Handle);
app.MapMethods(CourseDelete.Template, CourseDelete.Methods, CourseDelete.Handle);
#endregion


app.Run();
