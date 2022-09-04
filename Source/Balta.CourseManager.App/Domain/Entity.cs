namespace Balta.CourseManager.App.Domain;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    //Criado e Editado por um colaborador autorizado
    // public string CreatedBy { get; set; }
    // public DateTime CreatedOn { get; set; }

    // public string EditedBy { get; set; }
    // public DateTime EditedOn { get; set; }

}