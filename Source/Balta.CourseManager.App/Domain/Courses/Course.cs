namespace Balta.CourseManager.App.Domain.Courses;

public class Course : Entity
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Tag { get; set; }
    public double DurationInMinutes { get; set; }
    public bool Availability { get; set; } = true;
    public ICollection<Module> Modules { get; set; }
}

//Curso ADMiN
//Modulo 1,3,4,5