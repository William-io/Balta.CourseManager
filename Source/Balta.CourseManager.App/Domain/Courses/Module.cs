namespace Balta.CourseManager.App.Domain.Courses;

public class Module : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Guid CourseId { get; set; }

    //Varios modulos num curso == modulo 1, 2, 3, 4,
    public Course Courses { get; set; }
    public List<Learn> Learns { get; set; }

}



