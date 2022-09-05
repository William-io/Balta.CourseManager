using System.Collections.ObjectModel;
using Flunt.Validations;

namespace Balta.CourseManager.App.Domain.Courses;

public class Module : Entity
{
    public Module() { }
    public Module(string title, string description, Course course, Learn learn)
    {
        Title = title;
        Description = description;
        Course = course;
        Learn = learn;

        Validating();
    }

    private void Validating()
    {
        var contract = new Contract<Module>()
                    .IsNotNullOrEmpty(Title, "Title", "Titulo não pode ser vazio")
                    .IsGreaterOrEqualsThan(Title, 5, "Title", "Titulo precisa ser maior que 3 caracteres");
        AddNotifications(contract);
    }

    public void EditInfoToValidate(string title, string description)
    {
        Title = title;
        Description = description;

        Validating();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Course Course { get; private set; }
    public Guid CourseId { get; private set; }
    public Learn Learn { get; private set; }
}



