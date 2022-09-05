namespace Balta.CourseManager.App.Employess;

//Decido o que vai ser retornado no body da requisição, Evitando a senha & numero de permissão
public record EmployeeResponse(string Email, string Name);
