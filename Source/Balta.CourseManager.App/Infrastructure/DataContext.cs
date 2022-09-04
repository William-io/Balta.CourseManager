using Balta.CourseManager.App.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace Balta.CourseManager.App.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) { }


    //Todos os tipos STRING, precisam ter acima de 100 Caracteres
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Learn> Learns { get; set; }
    public DbSet<Module> Modules { get; set; }
}