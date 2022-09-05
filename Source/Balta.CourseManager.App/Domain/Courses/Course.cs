using Flunt.Validations;

namespace Balta.CourseManager.App.Domain.Courses;

public class Course : Entity
{
    public Course(string title, string summary, string tag, double durationInMinutes)
    {
        Title = title;
        Summary = summary;
        Tag = tag;
        DurationInMinutes = durationInMinutes;
        Availability = true;

        Validating();

    }

    //FLUNT
    private void Validating()
    {
        var contract = new Contract<Course>()
                    .IsNotNullOrEmpty(Title, "Title", "Titulo não pode ser vazio")
                    .IsGreaterOrEqualsThan(Title, 5, "Title", "Titulo precisa ser maior que 3 caracteres");
        AddNotifications(contract);
    }

    public string Title { get; private set; }
    public string Summary { get; private set; }
    public string Tag { get; private set; }
    public double DurationInMinutes { get; private set; }
    public bool Availability { get; private set; } = true;
}